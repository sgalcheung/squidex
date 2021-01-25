﻿// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using System.Threading.Tasks;
using Squidex.Domain.Apps.Core.Contents;
using Squidex.Domain.Apps.Entities.Contents.Commands;
using Squidex.Domain.Apps.Entities.Schemas.Commands;
using Squidex.Infrastructure;
using Squidex.Infrastructure.Commands;
using Squidex.Infrastructure.Reflection;

namespace Squidex.Domain.Apps.Entities.Contents
{
    public sealed class SingletonCommandMiddleware : ICommandMiddleware
    {
        public async Task HandleAsync(CommandContext context, NextDelegate next)
        {
            await next(context);

            if (context.IsCompleted &&
                context.Command is CreateSchema createSchema &&
                createSchema.IsSingleton)
            {
                var schemaId = NamedId.Of(createSchema.SchemaId, createSchema.Name);

                var data = new ContentData();

                var contentId = schemaId.Id;
                var content = new CreateContent
                {
                    Data = data,
                    ContentId = contentId,
                    DoNotScript = true,
                    DoNotValidate = true,
                    Publish = true,
                    SchemaId = schemaId
                };

                SimpleMapper.Map(createSchema, content);

                await context.CommandBus.PublishAsync(content);
            }
        }
    }
}
