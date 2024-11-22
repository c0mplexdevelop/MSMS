using Microsoft.EntityFrameworkCore;

namespace MSMS.Data;

public class EMSDatabaseContext(DbContextOptions<EMSDatabaseContext> options) : DbContext(options)
{

}
