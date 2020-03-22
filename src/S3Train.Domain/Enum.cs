namespace S3Train.Domain
{
    public enum ProductAdvertisementType
    {
        SliderBanner,
        WomenSquareBanner,
        MenSquareBanner,
        UnisexSquareBanner,
        MidVertRectangleBanner,
        LgVertRectangleBanner,
        MidHorRectangleBanner,
        LgHorRectangleBanner
    }

    public enum BannerType
    {
       WomenBanner,
       MenBanner,
       MainBanner
    }

    public enum OrderStatus
    {
       Receive,
       Confirm,
       TakeProduct,
       Delivery,
       Success,
       Cancel
    }
}