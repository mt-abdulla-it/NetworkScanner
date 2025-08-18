NetworkScanner - WPF Application

![.NET Version](https://img.shields.io/badge/.NET-6.0-blue)
![WPF](https://img.shields.io/badge/WPF-Microsoft-blueviolet)
![License](https://img.shields.io/badge/License-MIT-green)

A WPF application for network scanning and analysis, following MVVM architecture.

## Project Structure

| Directory/File | Purpose |
|----------------|---------|
| **/Models** | Data models and business logic |
| **/ViewModels** | MVVM viewmodel classes |
| **/Views** | XAML view definitions |
| **App.xaml** | Application resources and startup |
| **NetworkScanner.csproj** | Project configuration |
| **InverseBooleanConverter.cs** | Value converter utilities |

## Features
- Network device discovery
- Port scanning functionality
- MVVM pattern implementation
- Custom value converters

## Getting Started

### Prerequisites
- .NET 6.0 SDK
- Visual Studio 2022 (recommended)

### Installation
```bash
git clone https://github.com/mt-abdullah/NetworkScanner.git
cd NetworkScanner
```

### Building
```bash
dotnet build
```

## Usage
1. Run the application
2. Enter target IP range
3. Click "Scan" button

## Contributing
Pull requests are welcome. For major changes, please open an issue first.

## License
[MIT](https://github.com/mt-abdullah/NetworkScanner/blob/main/LICENSE)


Key features of this README:
1. *Badges* - Visual indicators for tech stack
2. **Structure Table** - Clearly documents your project organization
3. **Concise Instructions** - Easy setup and usage
4. **MVVM Acknowledgment** - Highlights your architecture pattern
5. **License Reference** - Proper open-source attribution

Would you like me to add any specific details about:
- The scanning functionality?
- Screenshot examples?
- Detailed configuration options?
