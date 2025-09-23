﻿using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace MyProjectName.WebApp.Blazor.Client.Pages;

public partial class Index
{
    [Inject]
    private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

    private ClaimsPrincipal? User { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        User = authState.User;
    }
}
