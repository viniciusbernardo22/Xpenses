﻿namespace Xpenses.API.Common.Api;

public interface IEndpoint
{
    static abstract void Map(IEndpointRouteBuilder app);
    
}