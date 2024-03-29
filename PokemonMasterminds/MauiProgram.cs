﻿using Microsoft.Extensions.Logging;
using PokemonMasterminds.Pages;
using PokemonMasterminds.ViewModels;
using CommunityToolkit.Maui;
using Plugin.Maui.Audio;

namespace PokemonMasterminds
{

    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddTransient<InputLobby>();
        builder.Services.AddTransient<InputLobbyViewModel>();

        builder.Services.AddSingleton((AudioManager.Current));
        builder.Services.AddTransient<MainPage>();

        return builder.Build();

        }
    }
}