using System;
using System.Collections.Generic;
using Data;

namespace Project {
    class Program {
        static void Main (string[] args) {
            var userService = new UserService ();

            userService.Create (new User () { UserName = "Anders" });

            List<User> users = userService.Get ();

            foreach (var user in users) {
                System.Console.WriteLine (user.UserName);
            }

            while (true) { }
        }
    }
}