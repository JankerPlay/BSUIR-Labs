﻿namespace app_253504_Frolenko.Application.WorkUseCases.Queries;
public sealed record GetWorkByIdQuery(int Id): IRequest<Work> { }