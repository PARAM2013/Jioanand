# Agent Environment Setup

This document provides instructions for setting up the development environment for this project.

## Overview

This is an ASP.NET Core project that requires the .NET 8 SDK. The agent environment is based on Ubuntu 24.04, but the .NET SDK is not pre-installed.

## Setup Instructions

To work on this project, you must first install the .NET 8 SDK. Run the following commands in the terminal:

```bash
# Update package lists
sudo apt update

# Install the .NET 8 SDK and runtime
sudo apt install -y dotnet8
```

After the installation is complete, you can verify it by running:

```bash
dotnet --version
```

This should output a version number starting with `8.x.x`.

Once the SDK is installed, you can build the project using the following command from the root directory:

```bash
dotnet build Jioanand.sln
```
