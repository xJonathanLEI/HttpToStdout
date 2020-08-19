# HTTP to Stdout

A simple .NET tool for printing incoming HTTP requests to console.

TODOs:

- Support HTTPS
- Support binary request body

## Installation

Install it as a global tool via [NuGet](https://www.nuget.org/packages/HttpToStdout):

    dotnet tool install -g HttpToStdout

## Usage

Start the server on default port (8080):

    http2stdout

Start the server on a custom port:

    http2stdout -p 3000

Simply press Ctrl+C to stop the server.