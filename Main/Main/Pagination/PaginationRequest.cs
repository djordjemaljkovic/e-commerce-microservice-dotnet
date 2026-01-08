namespace Main.Pagination
{
    //When querying for the complete data set, specify which page you want and how many orders per page
    public record PaginationRequest(int PageIndex = 0, int PageSize = 10);
}
