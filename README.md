# Contacts Management API

This project is a RESTful API built using .NET Core to manage contact information. It supports basic CRUD (Create, Read, Update, Delete) operations on contacts.

## Table of Contents

- [Technologies Used](#technologies-used)
- [Setup Instructions](#setup-instructions)
- [API Endpoints](#api-endpoints)
- [Error Handling](#error-handling)
- [Design Decisions](#design-decisions)
- [Future Enhancements](#future-enhancements)

## Technologies Used

- **Backend Framework**: .NET Core 6+
- **Data Storage**: Local JSON file
- **Error Handling**: Custom middleware for global error handling

## Setup Instructions

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) installed on your machine.

### Clone the Repository

```bash
git clone https://github.com/rajkrana01/contactapi.git
cd contacts-api
dotnet run

This will start the API, and it should be accessible http://localhost:5001
