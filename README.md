# evacuationApi
To run the project locally, follow the steps below.

### 1. Restore all dependencies
```bash
 dotnet restore 
```

### 2. Configure the database connection by creating or editing appsettings.Development.json in the evacuationApi project
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=EvacuationDb;Trusted_Connection=True;TrustServerCertificate=True;",
  },
   "Redis": {
       "ConnectionString": "localhost:6379"
  }
}
```

### 3. Apply database migrations
```bash 
dotnet ef database update --project Infrastructure --startup-project evacuationApi 
```

### 4. Run the API
```bash 
dotnet run --project evacuationApi 
```

### 5. Open Swagger UI at
`https://localhost:<port>/swagger`


## Example Data Create Vehicle
**POST** /api/vehicles

## Example 1 (Bus):
```json
{
  "capacity": 40,
  "vehicleTypeId": "8E637482-A0C2-45A2-B97E-0C8C87E1B120",
  "speed": 60,
  "latitude": 13.765000,
  "longitude": 100.538100
}
 ```
## Example 2 (Van):
```json
{
  "capacity": 20,
  "vehicleTypeId": "DDF12D9D-C3E0-460B-9730-145E2016C27D",
  "speed": 80,
  "latitude": 13.732000,
  "longitude": 100.520000
}
```
## Example Data Create Evacuation Zone
**POST** /api/evacuation-zones

## Example 1:
```json
{
  "latitude": 13.736700,
  "longitude": 100.523100,
  "numberOfPeople": 50,
  "urgencyLevel": 4
}
```
## Example 2:
```json
{
  "latitude": 13.756300,
  "longitude": 100.501800,
  "numberOfPeople": 100,
  "urgencyLevel": 5
}
```
