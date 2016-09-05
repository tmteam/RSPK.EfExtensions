
Tools for EntityFramework 6.0

To install RSPK.EfExtensions, run the following command in the Package Manager Console

    >: Install-Package RSPK.EfExtensions
    
    
## Examples ##

    var page = DataContext.Users
      .NotRemoved()//Entities that not marked as "removed"               
      .ThatWasCreated(includeFrom: from, excludeTo to) //Entities that was created in [in:to)
      .OrderBy(c=>c.Created)
      .Take(5);
    
    // Returns user with id equal to 123
    var usr = DataContext.Users.NotRemoved.Get(id: 123);
    
    // Marks user as removed
    usr.MarkAsRemoved();
    //or
    //Remove user from db
    usr.RemoveFromDb(); 
    
    // Return user with id equal to 456
    var usrOrNull = DataContext.Users.NotRemoved.GetOrNull(id: 456);
    
    // Return users which id is in interval [1..5]
    var someUsers = DataContext.Users.GetAnyOf(1,2,3,4,5);
    
    // Return users which id is in interval [1..5]. Throws if any ids were not found
    var someOtherUsers = DataContext.Users.GetAllOf(1,2,3,4,5);
    
    
    
