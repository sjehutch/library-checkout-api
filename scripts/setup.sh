#!/bin/bash

# Create solution and projects
dotnet new sln -n LibraryCheckout

dotnet new webapi -n LibraryCheckout.Api -o src/LibraryCheckout.Api --use-minimal-apis
dotnet new xunit -n LibraryCheckout.Api.Tests -o tests/LibraryCheckout.Api.Tests

# Add projects to solution
dotnet sln add src/LibraryCheckout.Api/LibraryCheckout.Api.csproj
dotnet sln add tests/LibraryCheckout.Api.Tests/LibraryCheckout.Api.Tests.csproj

# Add project reference
dotnet add tests/LibraryCheckout.Api.Tests reference src/LibraryCheckout.Api

# Initialize git (only if not already initialized)
if [ ! -d ".git" ]; then
  git init
  git checkout -b main
fi

# Create a basic .gitignore if it doesn't exist
if [ ! -f ".gitignore" ]; then
cat <<EOL > .gitignore
bin/
obj/
.vs/
.vscode/
*.user
*.suo
*.log
TestResults/
EOL
fi

# Initial commit
git add .
git commit -m "chore: initial solution scaffold"