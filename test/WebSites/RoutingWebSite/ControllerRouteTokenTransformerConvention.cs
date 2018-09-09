// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;

namespace RoutingWebSite
{
    public class ControllerRouteTokenTransformerConvention : IApplicationModelConvention
    {
        private readonly Type _controllerType;
        private readonly IParameterTransformer _parameterTransformer;

        public ControllerRouteTokenTransformerConvention(Type controllerType, IParameterTransformer parameterTransformer)
        {
            if (parameterTransformer == null)
            {
                throw new ArgumentNullException(nameof(parameterTransformer));
            }

            _controllerType = controllerType;
            _parameterTransformer = parameterTransformer;
        }


        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers.Where(c => c.ControllerType == _controllerType))
            {
                foreach (var controllerSelector in controller.Selectors)
                {
                    if (controllerSelector.AttributeRouteModel != null)
                    {
                        controllerSelector.AttributeRouteModel.RouteTokenTransformer = _parameterTransformer;
                    }
                }

                foreach (var action in controller.Actions)
                {
                    foreach (var actionSelector in action.Selectors)
                    {
                        if (actionSelector.AttributeRouteModel != null)
                        {
                            actionSelector.AttributeRouteModel.RouteTokenTransformer = _parameterTransformer;
                        }
                    }
                }
            }
        }
    }
}
