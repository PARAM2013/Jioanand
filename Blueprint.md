# Marriage Hall Room Booking Web Application - Development Blueprint

This document outlines the step-by-step plan for developing the Marriage Hall Room Booking web application. Each step is designed to be a self-contained module that can be implemented and tested independently.

## Step 1: Project Setup & Foundational Models

This step focuses on setting up the project environment and creating the core data models and basic UI for managing locations and rooms.

### 1.1. Project Structure
- Create the necessary folders for Controllers, Models, and Views.
- Configure the database connection string in `appsettings.json`.

### 1.2. Models
- Create the following Entity Framework models:
    - `Location`: To store details of each marriage hall location (e.g., Name, Address).
    - `Room`: To store details of each room (e.g., Room Number, Floor, Type, Capacity, Price, LocationId).

### 1.3. Controllers & Views
- Create a `LocationsController` with actions for:
    - `Index`: To list all locations.
    - `Create`: To add a new location.
    - `Edit`: To update an existing location.
    - `Delete`: To remove a location.
- Create corresponding Razor Views for each action.
- Create a `RoomsController` with actions for:
    - `Index`: To list all rooms, with filters for location and type.
    - `Create`: To add a new room.
    - `Edit`: To update an existing room.
    - `Delete`: To remove a room.
- Create corresponding Razor Views for each action.

### 1.4. Database
- Use Entity Framework Core to generate the database schema based on the models.
- Seed the database with some initial data for locations and rooms for testing purposes.

**Testing for Step 1:**
- Verify that all CRUD (Create, Read, Update, Delete) operations for Locations and Rooms are working correctly through the web interface.
- Ensure that the database is created with the correct schema and relationships.

---

## Step 2: Client Management

This step focuses on building the functionality to manage client information.

### 2.1. Model
- Create a `Client` model with the following properties:
    - `Id`: Unique identifier.
    - `FullName`: Client's full name.
    - `ContactNumber`: Client's phone number.
    - `Email`: Client's email address.
    - `Address`: Client's physical address.
    - `AlternateContact`: Alternate contact number.
    - `IdDocumentPath`: Path to the uploaded ID document.

### 2.2. Controller & Views
- Create a `ClientsController` with actions for:
    - `Index`: To list all clients with search functionality.
    - `Create`: To add a new client, including a file upload for the ID document.
    - `Edit`: To update a client's details.
    - `Details`: To view a client's information and their booking history.
    - `Delete`: To remove a client.
- Create corresponding Razor Views for each action.

### 2.3. File Handling
- Implement logic to handle file uploads for ID documents.
- Store the uploaded files in a designated folder (e.g., `wwwroot/uploads/ids`).
- Store the file path in the `IdDocumentPath` property of the `Client` model.

**Testing for Step 2:**
- Verify that all CRUD operations for Clients are working correctly.
- Test the file upload functionality to ensure that ID documents are uploaded and stored correctly.
- Test the search functionality for clients.

---

## Step 3: Booking Process - Core Functionality

This step implements the main booking workflow, from selecting rooms to recording payments.

### 3.1. Models
- Create a `Booking` model with properties like:
    - `Id`: Unique identifier.
    - `ClientId`: Foreign key to the `Client` model.
    - `BookingDate`: Date of the booking.
    - `CheckInDate`: Check-in date.
    - `CheckOutDate`: Check-out date.
    - `TotalCost`: Total cost of the booking.
    - `PaymentStatus`: Enum (e.g., `Paid`, `Partial`, `Pending`).
- Create a `BookingRoom` model to represent the many-to-many relationship between `Booking` and `Room`.

### 3.2. Controller & Views
- Create a `BookingsController` with actions for:
    - `Create`: A multi-step wizard for creating a new booking.
        - Step 1: Select or create a client.
        - Step 2: Choose dates and view available rooms in a visual grid.
        - Step 3: Select rooms and confirm the booking.
    - `Index`: To list all bookings with filters.
    - `Details`: To view the details of a specific booking.
    - `Cancel`: To cancel a booking.
- Create a `PaymentsController` to handle payment recording (cash only).

### 3.3. Visual Room Selection
- Implement a visual grid interface to display room availability.
- Use JavaScript to handle room selection and update the total cost in real-time.
- Clearly indicate the status of each room (Available, Booked, Selected).

**Testing for Step 3:**
- Test the booking creation wizard to ensure a smooth and intuitive user experience.
- Verify that the room availability is updated correctly after a booking is made.
- Test the payment recording functionality.

---

## Step 4: Dashboard & Reporting

This step focuses on creating a dashboard for business insights and generating basic reports.

### 4.1. Dashboard
- Create a `DashboardController` and a corresponding `Index` view.
- Display the following metrics on the dashboard:
    - Daily, weekly, and monthly booking summaries.
    - Room occupancy rates (per location).
    - Pending payments.
    - Recent booking activity.
- Use a charting library (e.g., Chart.js) to visualize the data.

### 4.2. Reporting
- Implement functionality to generate and export the following reports in CSV or Excel format:
    - `Booking Report`: A list of all bookings within a specified date range.
    - `Payment Report`: A list of all payments received within a specified date range.

**Testing for Step 4:**
- Verify that the dashboard displays accurate and up-to-date information.
- Test the report generation and export functionality.

---

## Step 5: Invoice Generation and Final Touches

This step covers the generation of GST-compliant invoices and adds the final polish to the application.

### 5.1. Invoice Generation
- Create a `InvoicesController`.
- Implement a feature to generate GST-compliant invoices in PDF format for fully paid bookings.
- The invoice should include:
    - Client details.
    - Booking details.
    - Room details.
    - Total amount with GST breakdown.
    - Payment details.
- Use a library like `Rotativa` or `iTextSharp` for PDF generation.

### 5.2. UI/UX Improvements
- Review and refine the overall user interface for consistency and ease of use.
- Add user-friendly error messages and validation.
- Ensure the application is responsive and works well on different screen sizes.

### 5.3. Future Enhancements (Preparation)
- Add placeholders in the UI for future features like user login/logout and role-based access control.
- Ensure the codebase is well-documented and modular to facilitate future development.

**Testing for Step 5:**
- Verify that the generated invoices are accurate and follow the GST format.
- Conduct a final round of user acceptance testing (UAT) to ensure the application meets all requirements.
