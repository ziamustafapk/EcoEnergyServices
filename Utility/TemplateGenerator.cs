using System.Text;
using YourProjectName.ViewModels.MyData;

namespace YourProjectName.Utility
{
    public static class TemplateGenerator
    {
        public static string GetHtmlString(IEnumerable<MyDataViewModel> myDataViewModels)
        {
            

            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>You Project Name PDF report!!!</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>Id</th>
                        <th>Name</th>
                        <th>Contact</th>
                        <th>Signature</th>
                                    </tr>");

            foreach (var myData in myDataViewModels)
            {
                string imagePath = "";
                if (myData.Signature != null)
                    imagePath = Convert.ToBase64String(myData.Signature);
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>
<img src=""data:image/jpg;base64,{3} height=""50"" width=""50"" />
</td>
                                  </tr>", myData.Id, myData.Name, myData.Contact, imagePath);
            }

            sb.Append(@"
                                </table>
                            </body>
                        </html>");

            return sb.ToString();
        }



    }
}
