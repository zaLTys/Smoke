﻿using Application.Abstractions.Messaging;
using Domain.Entities.Requests;

namespace Application.Features.Requests.HttpRequest.Commands.CreateApiRequest
{
    public record CreateApiRequestCommand(string Curl) : ICommand<ApiRequest>;
}
