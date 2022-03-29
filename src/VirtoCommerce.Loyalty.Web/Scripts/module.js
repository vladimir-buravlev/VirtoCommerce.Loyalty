// Call this to register your module to main application
var moduleName = 'Loyalty';

if (AppDependencies !== undefined) {
    AppDependencies.push(moduleName);
}

angular.module(moduleName, [])
    .config(['$stateProvider', '$urlRouterProvider',
        function ($stateProvider, $urlRouterProvider) {
            $stateProvider
                .state('workspace.LoyaltyState', {
                    url: '/Loyalty',
                    templateUrl: '$(Platform)/Scripts/common/templates/home.tpl.html',
                    controller: [
                        '$scope', 'platformWebApp.bladeNavigationService', function ($scope, bladeNavigationService) {
                            var newBlade = {
                                id: 'blade1',
                                controller: 'Loyalty.helloWorldController',
                                template: 'Modules/$(VirtoCommerce.Loyalty)/Scripts/blades/hello-world.html',
                                isClosingDisabled: true
                            };
                            bladeNavigationService.showBlade(newBlade);
                        }
                    ]
                });
        }
    ])
    .run(['platformWebApp.mainMenuService', 'platformWebApp.widgetService', '$state',
        function (mainMenuService, widgetService, $state) {
            //Register module in main menu
            var menuItem = {
                path: 'browse/Loyalty',
                icon: 'fa fa-cube',
                title: 'Loyalty',
                priority: 100,
                action: function () { $state.go('workspace.LoyaltyState'); },
                permission: 'Loyalty:access'
            };
            mainMenuService.addMenuItem(menuItem);
        }
    ]);
