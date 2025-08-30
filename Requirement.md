# Software Requirements Specification (SRS) for Marriage Hall Room Booking Web Application

## 1. Introduction

### 1.1 Purpose
This document outlines the requirements for a web application designed to manage room bookings for a marriage hall business. The application streamlines client management, booking processes, room inventory, payments, and reporting, primarily for managers and staff. It supports multiple locations and ensures GST-compliant invoicing.

### 1.2 Scope
The application includes:
- Client record management with personal details and booking history.
- Interactive booking interface with visual room selection.
- Room management across multiple locations.
- Payment tracking (cash-only, partial/full) and GST-compliant invoice generation.
- Dashboard for operational insights.
- Future enhancements for user authentication, reporting, notifications, and auditing.

Out of scope for the initial phase:
- Online payments (only cash is supported).
- Client self-service portal.
- Integration with external systems unless specified in future phases.

### 1.3 Definitions and Acronyms
- **Client**: Individual or group booking rooms for events like weddings.
- **Manager**: Primary user handling bookings, payments, and client interactions.
- **Administrator**: User managing room details (may overlap with manager initially).
- **GST**: Goods and Services Tax (India-specific tax compliance for invoices).
- **Aadhaar**: Indian national identity card (example of uploadable ID document).
- **Room Types**: Categories such as deluxe, standard, suite.
- **Booking History**: Record of past bookings for a client.
- **Partial Payment**: Advance or installment payments before full settlement.
- **Invoice**: GST-compliant document generated post-full payment.
- **Dashboard**: Centralized view of metrics and summaries.

### 1.4 References
- GST Invoice Format Guidelines (as per Indian tax laws).
- Similar systems: Movie ticket booking interfaces (e.g., BookMyShow for visual selection).

### 1.5 Overview
The application will be a web-based system built using modern web technologies. It will handle data securely, with file uploads for documents. Future phases will incorporate detailed role-based access control.

## 2. Overall Description

### 2.1 Product Perspective
The application replaces manual booking ledgers and spreadsheets, reducing errors in room availability tracking and payment reconciliation. It provides a visual, interactive room selection experience to enhance usability.

### 2.2 Product Functions
- Maintain client profiles and histories.
- Facilitate bookings with room visualization.
- Manage room inventory across locations.
- Track payments and generate invoices.
- Provide dashboards for monitoring.

### 2.3 User Classes and Characteristics
- **Managers**: Handle bookings, payments, and client interactions. Tech-savvy but not developers.
- **Administrators**: Manage room data. May require higher access.
- **Staff (Future)**: Limited access for viewing bookings.
- **Owner (Future)**: Receive email notifications.

### 2.4 Operating Environment
- Web browser (Chrome, Firefox, Edge) on desktops/laptops.

### 2.5 Design and Implementation Constraints
- Support only cash payments initially.
- Ensure GST compliance in invoices (include fields like GSTIN, HSN codes if applicable).
- Visual room selection similar to grid-based ticket booking systems.

### 2.6 Assumptions and Dependencies
- Users have internet access.
- ID documents are uploaded in standard formats (PDF, JPG, PNG).
- Multiple locations are predefined; dynamic addition in future.
- No real-time collaboration (single-user booking at a time).

## 3. Functional Requirements

### 3.1 Client Management
- **FR1.1**: Create client records with fields: Full Name, Contact Number, Email, Address, Alternate Contact.
- **FR1.2**: Support upload of identity documents (e.g., Aadhaar, Passport, any ID card). Store files securely with size limit (e.g., 5MB per file).
- **FR1.3**: View, edit, and delete client records.
- **FR1.4**: Maintain booking history for each client, showing past bookings, payments, and invoices.
- **FR1.5**: Search clients by name, contact, or booking ID.

### 3.2 Booking Process
- **FR2.1**: Step 1: Enter or select existing client details and upload/update documents.
- **FR2.2**: Step 2: Specify number of rooms required, booking dates (check-in/check-out), and event details (optional).
- **FR2.3**: Display available rooms in a visual grid/interface (e.g., floor-wise layout like movie seats). Rooms shown as clickable icons/cards with status (available/booked).
  - Filter by type, floor, capacity, price.
  - Highlight selected rooms.
- **FR2.4**: Confirm room selection and calculate total cost based on room prices and duration.
- **FR2.5**: Record payment: Support partial (advance) and full payments. Only cash mode. Track payment history with amounts, dates, and balances.
- **FR2.6**: After full payment, generate GST-compliant invoice including:
  - Client details, booking ID, rooms booked, total amount, GST breakdown, payment details.
  - Printable PDF format.
- **FR2.7**: Maintain annual summaries of bookings and payments for financial reporting (e.g., total revenue, taxes collected).
- **FR2.8**: Update room availability automatically upon booking confirmation.
- **FR2.9**: Handle cancellations: Refund logic (partial/full), update availability, and log in history.

### 3.3 Room Management
- **FR3.1**: Add new rooms with details: Room Number, Floor, Type (deluxe/standard/suite), Capacity (e.g., number of guests), Price (per day/night).
- **FR3.2**: Link rooms to specific locations (e.g., Hall A, Hall B). Support multiple locations.
- **FR3.3**: Update or delete room details.
- **FR3.4**: View room list per location with filters (type, availability).
- **FR3.5**: Automatic status update: Available, Booked, Maintenance (manual flag).

### 3.4 Dashboard
- **FR4.1**: Display daily, weekly, monthly booking summaries (e.g., number of bookings, revenue).
- **FR4.2**: Show room occupancy rates (e.g., percentage occupied per location).
- **FR4.3**: List pending payments and completed bookings.
- **FR4.4**: Location-wise breakdowns.
- **FR4.5**: Charts/graphs for visualization (e.g., bar charts for occupancy).

### 3.5 Role-Based Access Management (Future)
- **FR5.1**: Implement detailed role-based access control with user-wise and page-wise permissions:
  - **Roles**:
    - **Admin**: Full access to all features, including client management, bookings, room management, payments, invoices, and dashboard.
    - **Manager**: Access to client management, bookings, payments, and dashboard. Restricted from adding/editing/deleting rooms or managing users.
    - **Staff**: View-only access to bookings, client details, and dashboard. No edit or delete permissions.
  - **Page-Wise Permissions**:
    - **Client Management Page**:
      - Admin: Add, edit, delete, view clients.
      - Manager: Add, edit, view clients; no delete.
      - Staff: View only.
    - **Booking Page**:
      - Admin: Create, edit, cancel bookings, process payments, generate invoices.
      - Manager: Create, edit, cancel bookings, process payments; no invoice deletion.
      - Staff: View bookings only.
    - **Room Management Page**:
      - Admin: Add, edit, delete rooms.
      - Manager: View rooms only.
      - Staff: View rooms only.
    - **Dashboard Page**:
      - Admin: Full access to all metrics and summaries.
      - Manager: Access to booking and payment summaries.
      - Staff: Limited view of occupancy and booking summaries.
    - **User Management Page** (for role management):
      - Admin: Add, edit, delete users; assign roles; configure page-wise permissions.
      - Manager/Staff: No access.
  - **FR5.1.1**: User Management Interface:
    - Create users with fields: Username, Full Name, Email, Role.
    - Edit user details and reassign roles.
    - Delete users (soft delete to maintain audit trail).
    - Assign page-wise permissions via checkbox or dropdown (e.g., Add: Yes/No, Edit: Yes/No).
  - **FR5.1.2**: Authentication:
    - Login with username/password.
    - Role-based redirection to authorized pages.
    - Session management with logout functionality.
- **FR5.2**: Advanced Reporting:
  - Generate reports on bookings, payments, occupancy.
  - Export to Excel and PDF.
- **FR5.3**: Email Notifications:
  - Send booking confirmations and payment reminders to hall owner.
- **FR5.4**: Audit Trail:
  - Log all actions (bookings, edits, deletions) with timestamps and user IDs.

## 4. Non-Functional Requirements

### 4.1 Performance
- Response time: < 2 seconds for page loads and searches.
- Handle up to 100 concurrent users (scalable in future).
- Booking process completion in < 5 minutes.

### 4.2 Security
- Role-based access control (future).

### 4.3 Usability
- Intuitive UI with responsive design (mobile-friendly).
- Visual room selection grid with tooltips (e.g., hover for room details).
- Error messages for invalid inputs (e.g., overlapping bookings).

### 4.4 Reliability
- Data backup daily.
- 99% uptime.
- Handle failures gracefully (e.g., retry on database errors).

### 4.5 Maintainability
- Modular code structure.
- Use version control (Git).
- Documentation for APIs and database schema.

### 4.6 Compliance
- GST invoice format as per regulations.

## 5. Data Requirements

### 5.1 Database Schema Outline
- **Clients Table**: ID, Name, Contact, Email, Address, DocumentPaths (JSON/array).
- **Bookings Table**: ID, ClientID, Rooms (JSON/array of RoomIDs), Dates, TotalCost, PaymentStatus (Partial/Full), InvoicePath.
- **Rooms Table**: ID, LocationID, Number, Floor, Type, Capacity, Price, Status.
- **Locations Table**: ID, Name, Address.
- **Payments Table**: ID, BookingID, Amount, Date, Mode (Cash).
- **Users Table (Future)**: ID, Username, FullName, Email, Role, Permissions (JSON).
- **AuditLogs Table (Future)**: ID, Action, UserID, Timestamp.

### 5.2 Data Integrity
- Foreign keys for relationships (e.g., Booking to Client).
- Validation: Unique room numbers per location, positive prices.

## 6. User Interface Requirements
- **UI6.1**: Dashboard: Cards for metrics, charts using libraries like Chart.js.
- **UI6.2**: Booking Form: Stepper wizard (Client > Rooms > Payment > Confirm).
- **UI6.3**: Room Grid: Interactive canvas or div-based layout.
- **UI6.4**: Invoice Preview: Before generation, show editable preview.
- **UI6.5**: User Management (Future): Table for users with add/edit/delete buttons; permission matrix for page-wise access.

## 7. Testing Requirements
- Unit tests for core functions (e.g., payment calculation).
- Integration tests for booking flow.
- User acceptance testing with sample data.
- Test role-based access for correct permissions.

## 8. Deployment and Support
- Deploy to a web server.
- Provide user manual and training for managers.
- Support for bug fixes in initial 3 months.

This SRS serves as the blueprint for development. Any changes must be approved and documented in revisions.
