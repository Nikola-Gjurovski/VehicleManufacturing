# Vehicle Manufacturing

Welcome to the **Vehicle Manufacturing** project, a .NET Core application developed for my university project, hosted on **Azure**. This application allows users to customize their own vehicle, select parts, and initiate manufacturing requests—all while handling payments securely through **Stripe**.

## Key Features

- **Custom Vehicle Manufacturing**: Users can choose their desired vehicle model and customize its parts (engines, chassis, doors, wheels, etc.).
- **Online Payment Integration**: Secure payment processing for orders is handled via **Stripe**.
- **Azure Data Factory**: Utilizes Azure Data Factory as an ETL method to connect and transform data from multiple team databases for use within the project.
- **Admin Panel**: Admins have access to a dedicated panel where they can approve manufacturing requests and track the process.
- **Automated Notifications**: Once a vehicle is manufactured, users receive an email confirmation of the completion.

This project not only demonstrates modern .NET Core web development but also integrates cloud-based data solutions, ensuring scalability and efficient data handling.
[Link to the web application](https://lastvehicle.azurewebsites.net/)
### Configuration
1. **Database Connection**
   - To run the Vehicle Manufacturing project , you need to create an `appsettings.json`file in the `VehicleWeb` directory. of the project with your own configurations.
     ```json
       {
       "ConnectionStrings": {
         "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DATABASE;Trusted_Connection=True;TrustServerCertificate=True;"
       },
       "Stripe": {
         "PublishableKey": "YOUR_PUBLISHABLE_KEY",
         "SecretKey": "YOUR_SECRET_KEY"
       },
       "Logging": {
         "LogLevel": {
           "Default": "Information",
           "Microsoft.AspNetCore": "Warning"
         }
       },
       "AllowedHosts": "*"
     }
     ```
  - Replace `YOUR_SERVER` with your own SQL Server instance.
  - Replace `YOUR_DATABASE` with the name of your database.
  - Replace `YOUR_PUBLISHABLE_KEY` with your own publishable key from Stripe.
  - Replace `YOUR_SECRET_KEY` with your secret key from Stripe.

### Setting Up the Database
2. Open your terminal and navigate to the `VehicleRepository` :
3. To set up the initial database schema, run the following commands:
```bash
add-migration init
update-database
```
