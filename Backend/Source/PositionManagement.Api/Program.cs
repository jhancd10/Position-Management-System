using PositionManagement.Infrastructure.DependencyInjection.Setup;

// Create and configure the default web application using the provided arguments
var app = DefaultWebApplication.Create(args);

// Run the configured web application
DefaultWebApplication.Run(app);
