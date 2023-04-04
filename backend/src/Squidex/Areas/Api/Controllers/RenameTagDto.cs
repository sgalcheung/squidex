﻿// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using Squidex.Infrastructure.Validation;
using Squidex.Web;

namespace Squidex.Areas.Api.Controllers;

[OpenApiRequest]
public sealed class RenameTagDto
{
    /// <summary>
    /// The new name for the tag.
    /// </summary>
    [LocalizedRequired]
    public string TagName { get; set; }
}
