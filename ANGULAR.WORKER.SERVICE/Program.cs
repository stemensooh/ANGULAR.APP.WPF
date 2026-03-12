using ANGULAR.WORKER.SERVICE;
using System.Drawing.Printing;

var builder = Host.CreateApplicationBuilder(args);

// Permite usar endpoints web
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();







app.Run();
