var builder = DistributedApplication.CreateBuilder(args);

var productApi = builder.AddProject<Projects.Ecommerce_ProductService>("api-product");
var orderApi = builder.AddProject<Projects.Ecommerce_OrderService>("api-order");

builder.AddProject<Projects.Ecommerce_Web>("front-web")
	.WithReference(productApi)
	.WithReference(orderApi);

builder.Build().Run();
