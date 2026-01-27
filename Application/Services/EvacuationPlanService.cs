using evacuation.Application.DTOs.EvacuationPlan;
using evacuation.Application.DTOs.EvacuationZones;
using evacuation.Application.DTOs.Vehicles;
using evacuation.Application.Helpers;
using evacuation.Application.Interfaces;
using evacuation.Application.Mappers;
using evacuation.Domain.Entities;
using evacuation.Domain.Interfaces;
using System.Collections.Generic;
using System.Numerics;




namespace evacuation.Application.Services
{
    public class EvacuationPlanService : IEvacuationPlanService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEvacuationZonesRepository _evacuationZonesRepo;
        private readonly IEvacuationPlansRepository _evacuationPlansRepo;
        private readonly IEvacuationTripsRepository _evacuationTripsRepo;
        private readonly IEvacuationStatusesRepository _evacuationStatusesRepo;
        private readonly IVehiclesRepository _vehiclesRepo;
        private readonly IRunningCodesRepository _runningCodesRepo;
        private readonly IEvacuationStatusCache _evacuationStatusCache;
        public EvacuationPlanService(
        IUnitOfWork unitOfWork,
        IEvacuationZonesRepository evacuationZonesRepo,
        IEvacuationPlansRepository evacuationPlansRepo,
        IEvacuationTripsRepository evacuationTripsRepo,
        IEvacuationStatusesRepository evacuationStatusesRepo,
        IVehiclesRepository vehiclesRepo,
        IRunningCodesRepository runningCodesRepo,
        IEvacuationStatusCache evacuationStatusCache)
        {
            _unitOfWork = unitOfWork;
            _evacuationZonesRepo = evacuationZonesRepo;
            _evacuationPlansRepo = evacuationPlansRepo;
            _evacuationTripsRepo = evacuationTripsRepo;
            _evacuationStatusesRepo = evacuationStatusesRepo;
            _vehiclesRepo = vehiclesRepo;
            _runningCodesRepo = runningCodesRepo;
            _evacuationStatusCache = evacuationStatusCache;
        }

        public async Task<bool> CreatePlans()
        {
   
            //1.ดึงพื้นที่อพยพทั้งหมด เรียงตาม urgency
            //2.ดึง vehicle ที่ว่างสามารถใช้งานได้
            //3.หาระยะทางจาก Zone กับแต่ละ vehicle ว่าระยะห่างเท่าไหร่
            //4.หา ETA จาก ระยะทาง/speed
            //5.assign  vehicle ให้ zone ด้วยเงื่อนไข eta น้อยสุด และ  Capacity >สุด

            List<EvacuationPlanResponseDto> responseDtos = new List<EvacuationPlanResponseDto>();

            //สถานะเริ่มต้นจาก DB

            var status = await _evacuationStatusesRepo.GetInitialStatusAsync();
            if (status == null)
                return false;

            Guid statusId = status.Id;

            var evacuationZones = await _evacuationZonesRepo.GetAllOrderedByUrgencyAsync();

            var vehicles = await _vehiclesRepo.GetAvailableVehiclesAsync();


            if (!vehicles.Any())
                return false;

            var running = await _runningCodesRepo.GetNextAsync("plan");
            string planCode = $"{running.prefix}{running.CurrentValue:D3}";

            foreach (var z in evacuationZones)
            {
                if (!vehicles.Any())
                    break; // ไม่มีรถเหลือแล้ว

                Guid zoneId = z.Id;
                double zoneLat = (double)z.Latitude;
                double zoneLong = (double)z.Longitude;
                int numberOfPeople = z.NumberOfPeople;

                List<VehicleResponseDto> vehicleTemp = new List<VehicleResponseDto>();

                foreach (var v in vehicles)
                {

                    string vCode = v.VehicleCode;
                    double vehicleLat = (double)v.Latitude;
                    double vehicleLonge = (double)v.Longitude;
                    double speedKmPerHour = (double)v.Speed;
                    int capacity = v.Capacity;
                    double distanceKm = DistanceCalculator.CalculateDistanceKm(vehicleLat, vehicleLonge, zoneLat, zoneLong);
                    double eta = DistanceCalculator.CalculateEtaMinutes(distanceKm, speedKmPerHour);

                    vehicleTemp.Add(
                        new
                        VehicleResponseDto
                        { Id = v.Id, VehicleCode = vCode, ETA = eta, Capacity = capacity });

                }

                //เลือกรถด้วยเงื่อนไข eta น้อยสุด และ  Capacity >สุด
                var selectedVehicle = vehicleTemp
                 .OrderBy(v => v.ETA)
                 .ThenByDescending(v => v.Capacity)
                 .FirstOrDefault();

                var evacuationPlan = new EvacuationPlan
                {
                    Id = Guid.NewGuid(),
                    PlanCode = planCode,
                    ZoneId = zoneId,
                    VehicleId = selectedVehicle.Id,
                    ETA = (decimal)selectedVehicle.ETA,
                    AssignedPeople = numberOfPeople,
                    StatusId = statusId,
                    CreateDate = DateTime.UtcNow,
                    IsActive = true,
                };


                await _evacuationPlansRepo.AddAsync(evacuationPlan);


                //ลบ vehicle ที่ถูกเลือกแล้วออกจาก vehicles
                vehicles.RemoveAll(v => v.Id == selectedVehicle.Id);


                #region update status plan in radis
                await _evacuationStatusCache.InitializeZoneAsync(evacuationPlan.Id.ToString(), z.ZoneCode, numberOfPeople);
                #endregion

            }
            await _unitOfWork.SaveChangesAsync();

            return true;

        }
        public async Task<IReadOnlyList<EvacuationPlanResponseDto>> GetActivePlansAsync()
        {
            var plans = await _evacuationPlansRepo.GetActiveAsync();

            return plans.Select(p => new EvacuationPlanResponseDto
            {
                PlanId = p.Id.ToString(),
                ZoneCode = p.Zone?.ZoneCode ?? string.Empty,
                VehicleCode = p.Vehicle?.VehicleCode ?? string.Empty,
                ETA = p.ETA,
                AssignedPeople = p.AssignedPeople
            }).ToList();
        }

        public async Task<IReadOnlyList<EvacuationStatusDto>> GetStatusPlan()
        {

            return await _evacuationStatusCache.GetAllAsync();
        }
        public async Task<bool> UpdatStatus(Guid planId, UpdateEvacuationPlanDto evacuationPlanDto)
        {

            //Guid planId = evacuationPlanDto.PlanId;
            int numberOfEvacuees = evacuationPlanDto.NumberOfEvacuees;
            string vehicleCode = evacuationPlanDto.VehicleCode;

            var vehicle = await _vehiclesRepo.GetByVehicleCodeAsync(vehicleCode.ToUpper());
            Guid? vehicleId = vehicle?.Id; //รถเป็นค่าว่างได้

            var plan = await _evacuationPlansRepo.GetByIdAsync(planId);
            if (plan == null) return false;

            IReadOnlyList<EvacuationTrip> trips = await _evacuationTripsRepo.GetByPlanIdAsync(planId);


            int evacuated = trips.Sum(t => t.PeopleCount); //จำนวนคนที่อพยพมาแล้วทั้งหมด

            int lastSequence = trips.Any()
                ? trips.Max(t => t.TripSequence)
                : 0;


            #region เพิ่มข้อมูลเที่ยวรถ
            var evacuationTrip = new EvacuationTrip
            {
                Id = Guid.NewGuid(),
                PlanId = planId,
                VehicleId = vehicleId,
                PeopleCount = numberOfEvacuees,
                TripSequence = lastSequence + 1,
                EndTime = DateTime.UtcNow,
                CreateDate = DateTime.UtcNow
            };
            await _evacuationTripsRepo.AddAsync(evacuationTrip);

            #endregion

            #region update status plan
            bool evacuationSuccess = (evacuated + numberOfEvacuees) >= plan.AssignedPeople;//จำนวนคนทั้งหมดที่อพยพมาแล้ว+จำนวนคนรอบนี้ > จำนวนคนทั้งหมดที่ต้องอพยพ
            var status = await _evacuationStatusesRepo.GetInitialStatusAsync();
            Guid firstStatusId = status.Id;

            bool ready = (plan.StatusId == firstStatusId);

            if (evacuationSuccess || ready)
            {
                var next = await _evacuationStatusesRepo.GetNextStatusAsync(plan.StatusId);
                if (next != null)
                {
                    plan.StatusId = next.Id;
                    plan.UpdateDate = DateTime.UtcNow;
                }
            }
            #endregion
            await _unitOfWork.SaveChangesAsync();



            #region update status plan in radis
            var zone = await _evacuationZonesRepo.GetByIdAsync(plan.ZoneId);
            string zoneCode = zone.ZoneCode;
            await _evacuationStatusCache.UpdateAsync(zoneCode, numberOfEvacuees, vehicleCode);
            #endregion

            return true;
        }


        public async Task ClearPlans()
        {

            await _evacuationStatusCache.ClearAsync();
            await _evacuationPlansRepo.SetInActiveAsync();

        }


    }
}
