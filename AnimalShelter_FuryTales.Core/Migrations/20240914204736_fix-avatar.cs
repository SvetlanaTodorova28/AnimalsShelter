using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalShelter_FuryTales.Core.Migrations
{
    public partial class fixavatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000020",
                column: "ConcurrencyStamp",
                value: "f9ad933b-16de-4fea-a7fa-3e41217d75b4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000021",
                column: "ConcurrencyStamp",
                value: "187a7ae0-f1d8-4787-832f-97bebfafbbd7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000027",
                column: "ConcurrencyStamp",
                value: "d8dd054c-8e1e-4411-8e20-8319fd5a6ac6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000014",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEIjEcxxzfnUcEtarzFqEtvQcQFFWF01ANKtA5luey+VZ15pMMYCjt5P647a7ttqocQ==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000015",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAENpNFy99tncRgQP3dwX1ahBMZFkXCo941O6ZpzTu9er7n0XHfDoVVtxT/bVu1zuncQ==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000022",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEHmJ+54Q07HdyB/NIHN2sDxq6sl+0tPn43kME7AQ85Efd8JsH9mylvhnrljMu1lRog==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000023",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEOXWTkIIMaihaWOnAaIxZ4AOssnYEvwYVNr3JBfGA61nKt9VjyGkfXKl2jJImuBEmg==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000024",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEHvfFnLikw9MwivMtHsQ/x/UBQqTiCA+z1L2ZK7dbyEvoNYd1W9dFRMBRZ1caoawIA==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000025",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEO0L6g/amz69NOo11vExc1kOtrs057cMHaLHJjQcWoZb4Wkn+RoFaukbXNqqKYgGYg==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000035",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEKkk2YUoYtNLFoPC/vkn3f1vaz8lbrd1l/o8g2sAV2ivaHWnXjStqRXNoWfr15FvSQ==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000020",
                column: "ConcurrencyStamp",
                value: "b252b734-e3cc-46ed-9cea-3628e9f8e099");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000021",
                column: "ConcurrencyStamp",
                value: "5ba48354-8e44-468c-bf05-150c1fe14d6a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000027",
                column: "ConcurrencyStamp",
                value: "ef5572c6-17ce-4470-97cf-415371bc5327");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000014",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEMo/nM9GfJtQvce05jvutpBQPX1WvtEGzTtQUIIDfirHmYEamlFR/o/3QaftVXqgBw==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000015",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAED4RsQiKJhzxUWS94cx4oOYx+uuWt4GMpH5t1a8yexJfE4HxtDGFFZ8owxgpN0Cqgg==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000022",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEBVfxCGgjSanvz+5atai6XGiJ/u/DTU1LaR7RlIqH9Q3053zzIXNtWMAR3EGoNt/Yw==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000023",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAENl2+LAEW2SCes/0+TLjMub7YYrE6YPIABdYtNF+spHFGD/rCU+OAbE5uaPa69wUKg==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000024",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEOVRBtlJwN1okC0ONqKdhPw35jwIpOQlJ9wvYS9mEmmdrdx8Kgy04hfYBPiH0FBUcA==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000025",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEDU2B05EIjkSKkNYO7VibxZEAOOKfbUyTt4wVe2fE1GKbPIEydGz2YQ+Ni3+j1KCUg==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000035",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEPqHW7KOJoki6I8+p483YsT5vNlRITbx93weYLFJdQrBIuK5dQAXOET5EiRbumN3hg==");
        }
    }
}
