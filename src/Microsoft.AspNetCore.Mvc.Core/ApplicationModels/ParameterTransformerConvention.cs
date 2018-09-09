// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;

namespace Microsoft.AspNetCore.Mvc.Core.ApplicationModels
{
    public sealed class RouteTokenTransformerConvention : IApplicationModelConvention
    {
        private readonly IParameterTransformer _parameterTransformer;

        public RouteTokenTransformerConvention(IParameterTransformer parameterTransformer)
        {
            if (parameterTransformer == null)
            {
                throw new ArgumentNullException(nameof(parameterTransformer));
            }

            _parameterTransformer = parameterTransformer;
        }

        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                foreach (var action in controller.Actions)
                {
                    foreach (var selector in action.Selectors)
                    {
                        selector.AttributeRouteModel.RouteTokenTransformer = _parameterTransformer;
                    }
                }
            }
        }
    }
}
