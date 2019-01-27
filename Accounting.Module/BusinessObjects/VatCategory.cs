namespace Accounting.Module.BusinessObjects
{
    public enum VatCategory
    {
        None,
        DeliveriesOrServicesTaxedAtHighRate,
        DeliveriesOrServicesTaxedAtLowRate,
        DeliveriesOrServicesTaxedAtOtherRates,
        PrivateUse,
        DeliveriesOrServicesUntaxed,
        DeliveriesOrServicesReverseCharged,
        DeliveriesToCountriesOutsideTheEuropeanUnion,
        DeliveriesOrServicesToCountriesWithinTheEuropeanUnion,
        InstallationOrDistanceSalesWithinTheEuropeanUnion,
        DeliveriesOrServicesFromCountriesOutsideTheEuropeanUnion,
        DeliveriesOrServicesFromCountriesWithinTheEuropeanUnion,
        InputVat,
        SmallBusinessScheme
    }
}