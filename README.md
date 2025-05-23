# BlazorProjectApp1 README
See below for more information and instructions please.
* For best performance use Mozilla Firefox browser.
* App URL: https://localhost:7222/

___

## Setup Instructions

* Extract ZIP file.
* Open file `BlazorProjectApp1.sln` in your IDE.
* Make sure startup item is: BlazorProjectApp1
* Make sure run option is: https

   ```bash
   dotnet restore
   ```
   ```bash
   dotnet build
   ```
___

## Getting Started
- Login with user account or register for a user account.
- Administrator Account: "Admin" <"Pass1234">
- User Account: "User" <"Pass1234">
- All accounts in developer preview use password "Pass1234".
- Users can only see/interact with their own posts and administrators can see/interact with all posts.
- Once logged in Create, Read, Update, and Delete posts.
- Created posts can have title and content changes and images uploaded to database.
- Upon image upload text in image is extracted via OCR.
- Once post is created with an OCR extracted image click View.
- In the View page you can play, pause, stop and use the audio progress bar to skip back and forward.
___

## Project Structure

```
BlazorProjectApp1/
├── github/                           # Github workflow YAML files.
├── BlazorProjectApp1/                # Core logic and razor pages.
├── BlazorProjectApp1.Tests/          # Unit tests.
├── .gitignore                        # Git file/paths to ignore.
├── .styleignore                      # Ignore files/paths list for linter. 
├── BlazorProjectApp1.sln             # Solution file.
├── README.md                         # README documentation.

```
___

## Features

- Login and register.
- SQLite database persistency.
- User account info with GUID stored in UserAccount in "\BlazorProjectApp1\BlazorProjectApp1\BlazorProjectApp.db".
- CRUD functionality for posts.
- Posts linked to user with unique PostId.
- Upload image files.
- ImageFile stored in "\BlazorProjectApp1\BlazorProjectApp1\BlazorProjectApp.db".
- OCRImageText stored in "\BlazorProjectApp1\BlazorProjectApp1\BlazorProjectApp.db".
- Binary audio data stored in RawAudioData in "\BlazorProjectApp1\BlazorProjectApp1\BlazorProjectApp.db".
- EdgeTTS AI audio playback.
- Play, Pause, Stop functionality.
- Audio progress bar.
- Search bar.
- CI/CD workflows.
- Initial Docker implementation.
- Scalability and prepared for further development.
___

## Technologies Used

- C#: Core programming language.

- Razor: Syntax for embedding code.

- Javascript: Programming language for audio player.

- CSS: Stylesheet language.

- Docker: For Docker CI/CD implementation.

- .NET Core: Framework for building and running the application.

- xUnit: Testing framework for unit tests.

- bUnit: Testing framework for unit tests for Blazor.
___

This repo utilises [Tesseract](https://www.nuget.org/packages/tesseract/) and [EdgeTTS](https://www.nuget.org/packages/EdgeTTS).
