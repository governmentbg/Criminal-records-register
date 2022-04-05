// See https://aka.ms/new-console-template for more information
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using DBProject;

Console.WriteLine("Hello, World!");

IConfigurationBuilder configBuilder = new ConfigurationBuilder().AddJsonFile("appSettings.json");
IConfiguration config =  configBuilder.Build();

Fbbc.GetData(config.GetConnectionString("FBBCConnectionString"), config.GetConnectionString("DefaultConnectionString"));
