# SpeedyAir Project

## Overview

The SpeedyAir project aims to provide efficient and fast air freight services, automating the scheduling and loading of orders onto flights. This repository includes the core functionalities for loading orders from JSON files, scheduling flights, and managing order assignments based on flight capacities.

## Features

- Load orders from a JSON file.
- Handle different destinations for orders.
- Efficient management of flight orders.

## Getting Started

### Prerequisites

- .NET SDK (version 6.0 or later)
- NuGet packages:
  - `Newtonsoft.Json`
  - `Moq`
  - `xUnit`

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/tahurasaiyed/SpeedyAir.git
   cd SpeedyAir
   ```

2. Restore the NuGet packages:
    ```bash
   dotnet restore
   ```

3. Build the project:
   ```bash
   dotnet build
   ```
   
## Testing

### Test Cases
Test cases are implemented using xUnit and Moq. The test cases check for both positive and negative scenarios as per the requirements.

### Running Tests
To run the tests, execute the following command:

```bash
dotnet test
```

## Contributions by [Tahura Saiyed](https://github.com/tahura-saiyed)

Tahura Saiyed has made significant contributions to this project, focusing on:

- **Feature Development**: Implemented features according to the business requirements.
- **Code Review and Optimization**: Reviewed code for best practices and improved performance.
- **Testing and Debugging**: Applied TDD practices to enhance code reliability, including unit tests using XUnit.