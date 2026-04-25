# Project memory
- First inspect the repository structure before editing anything.
- Keep naming consistent with the existing codebase.
- Prefer simple direct solutions.
- Avoid risky changes unless I explicitly ask.
- Tell me which files you want to read first.

## Main Projects
- **SmartRigWeb**: The Backend API. Handles data persistence (MS Access), filtering, and sorting logic.
- **WebSmartRig**: The Frontend MVC Application. Provides the user interface and communicates with the backend via `ApiClient`.
- **Models**: Shared project containing data models and ViewModels used across the solution.
- **ApiClient**: A helper library for making HTTP requests to the backend API.
- **SmartRigWPF**: A desktop management application for administrators.

## Core Workflows

### Catalog & Filtering Flow
1. **User Interaction**: User navigates to the catalog or changes filters in `WebSmartRig/Views/Guest/GetCatalog.cshtml`.
2. **Frontend Controller**: `WebSmartRig/Controllers/GuestController.GetCatalog` receives parameters and forwards them via `WebClient` to the backend.
3. **Backend API**: `SmartRigWeb/Controllers/GuestController.GetCatalog` receives the request.
4. **Filtering**: Handled via SQL `WHERE` clauses in `ComputerRepository.cs`. The backend controller uses a large `if/else if` chain to select the correct repository method based on the combination of active filters (Price, Company, OS, Type).
5. **Sorting**: Sorting (Price Low-to-High / High-to-Low) is performed manually in the backend `GuestController.cs` using a bubble sort algorithm after data is retrieved.
6. **Rendering**: Data is returned as a `CatalogViewModel` and rendered in the frontend view.

### Computer Details Flow
1. **User Interaction**: User clicks a computer card in the catalog.
2. **Frontend Controller**: `WebSmartRig/Controllers/GuestController.GetComputer` calls the backend with the `computerId`.
3. **Backend API**: `SmartRigWeb/Controllers/GuestController.GetComputerDetails` aggregates data for all computer components (CPU, GPU, RAM, etc.).
4. **Rendering**: Displayed using `WebSmartRig/Views/Guest/GetComputer.cshtml`.

## Important Files
- **Catalog View**: `WebSmartRig/Views/Guest/GetCatalog.cshtml`
- **Computer Details View**: `WebSmartRig/Views/Guest/GetComputer.cshtml`
- **Backend Controller**: `SmartRigWeb/Controllers/GuestController.cs`
- **Backend Repository**: `SmartRigWeb/DataAccessLayer/Repositorys/ComputerRepository.cs`

## Endpoints (Called by MVC App)
- **Catalog**: `api/Guest/GetCatalog`
- **Details**: `api/Guest/GetComputerDetails`

## Technical Debt & Warnings
- **Filtering Logic**: The catalog filtering is currently implemented using a "brute-force" combination strategy. `SmartRigWeb/Controllers/GuestController.cs` contains a massive `if/else if` chain to handle every possible permutation of filters, each calling a specific hardcoded method in `ComputerRepository.cs`. This is difficult to maintain or extend.
- **Sorting**: Sorting is done in memory on the API server using a bubble sort rather than via SQL `ORDER BY`.

## Catalog filter method map
- **GetByPriceRange**: `minPrice` + `maxPrice`
- **GetComputersByCompanyId**: `companyId`
- **GetComputersByOperatingSystemId**: `operatingSystemId`
- **GetComputerByType**: `typeId`
- **GetByPriceRangeAndCompanyId**: `minPrice` + `maxPrice` + `companyId`
- **GetByPriceRangeAndOperatingSystemId**: `minPrice` + `maxPrice` + `operatingSystemId`
- **GetByPriceRangeAndTypeId**: `minPrice` + `maxPrice` + `typeId`
- **GetComputersByCompanyIdAndOperatingSystemId**: `companyId` + `operatingSystemId`
- **GetComputersByCompanyIdAndTypeId**: `companyId` + `typeId`
- **GetComputersByOperatingSystemIdAndTypeId**: `operatingSystemId` + `typeId`
- **GetByPriceRangeAndCompanyIdAndOperatingSystemId**: `minPrice` + `maxPrice` + `companyId` + `operatingSystemId`
- **GetByPriceRangeAndCompanyIdAndTypeId**: `minPrice` + `maxPrice` + `companyId` + `typeId`
- **GetByPriceRangeAndOperatingSystemIdAndTypeId**: `minPrice` + `maxPrice` + `operatingSystemId` + `typeId`
- **GetByPriceRangeAndCompanyIdAndOperatingSystemIdAndTypeId**: `minPrice` + `maxPrice` + `companyId` + `operatingSystemId` + `typeId`
- **GetAll**: No filters (Default)
