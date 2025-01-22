# Otkupni Blokovi Management System

## Setup Instructions

1. **Clone the Repository**:
   ```bash
   git clone git@github.com:imchergar/zelene-teh.git
   ```

2. **Set Up the Database**:
    - Use SQL Server.

3. **Run Migrations**:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

4. **Run the Application**:
   ```bash
   dotnet run
   ```

5. **Access the Application**:
    - Navigate to `http://localhost:5234/` in your browser.

---


** Dodatne funkcionalnosti**:

Logiranje errora
Hardkodirane vrjednosti staviti u constante negdje u neki shareani file
Authentifikacija novih firmi
Registracija novih firmi
Kod editiranja opcija za unset stavki sa otkupnog bloka
Setanje vise od jedne stavke na otkupni blok



## Contact
For any questions or issues, please contact [imchergar](mailto:ivanmihael.cergar@gmail.com).
