param location string = resourceGroup().location
param sku string = 'F1'

var appNames =[
   'mvc'
   'postulation'
   'emplois'
   'documents'
   'favoris'
]


module AppServices 'Mod/appService.bicep' =  {
  name: 'appService'
  params: {
    appNames : appNames
    location: location
    sku: sku
  }
}
