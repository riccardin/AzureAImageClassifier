# .NET Core Command-Line Application for Image Classification using Azure AI

## Project Description

This project is a .NET Core command-line application that uses Azure Cognitive Services to identify the content of provided images and classify them into multiple predefined categories. The available categories for classification include:
- People
- Technology
- Post-it
- Faces
- Feelings

The application analyzes the images and returns the maximum probability for each category.

## Setup Instructions

### Prerequisites

- .NET Core SDK installed on your machine
- An Azure account

### Setting up Azure Cognitive Services

1. Sign in to the [Azure portal](https://portal.azure.com/).
2. In the left-hand navigation pane, select "Create a resource".
3. Search for "Cognitive Services" and select it.
4. Click "Create".
5. Fill in the required details, such as Subscription, Resource group, and Region.
6. Under "Kind", select "Computer Vision".
7. Click "Review + create" and then "Create".
8. Once the resource is created, go to the resource's overview page.
9. Copy the "Endpoint" and "Key" from the resource's "Keys and Endpoint" section.

## Running the Application

1. Clone the repository:
   ```
   git clone https://github.com/githubnext/workspace-blank.git
   ```
2. Navigate to the project directory:
   ```
   cd workspace-blank
   ```
3. Restore the required NuGet packages:
   ```
   dotnet restore
   ```
4. Build the project:
   ```
   dotnet build
   ```
5. Run the application with the image file paths as arguments:
   ```
   dotnet run -- <image-file-path-1> <image-file-path-2> ...
   ```

The application will analyze the provided images and display the classification results in the command-line interface.
