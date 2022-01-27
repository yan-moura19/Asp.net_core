namespace BackEnd.Pages
{
    public class Pages
    {
        public int TotalProdutos { get; private set; }
        public int AtualPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPages { get; private set; }
        public int EndPage { get; private set; }

        public Pages()
        {

        }
        public Pages(int totalProdutos, int page,int pageSize = 10)
        {
            int totalPages = (int)Math.Ceiling((decimal)TotalProdutos / (decimal)pageSize);
            int atualPage = page;

            int startpage = AtualPage -5;
            int endPage = AtualPage + 4;

            if (startpage <= 0)
            {
                EndPage = EndPage - (startpage - 1);
                StartPages = 1;
            }
            if (EndPage > totalPages)
            {
                EndPage = totalPages;
                if (EndPage>10)
                {
                    startpage = EndPage - 9;
                }
            }
            TotalProdutos = totalProdutos;
            AtualPage = atualPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPages = startpage;
            EndPage = endPage;



        }
    }
}
