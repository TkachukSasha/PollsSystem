## Review & Description
A web app that allows manipulating tests.  
In that platform you may create, edit, delete tests, and also look at and export statistics that you want.
The platform is reliable because it provides important points, such as user authorization, the ability to pass the test under your account no more than once, as well as random generation of questions on the test, which provides greater complexity and fairness of the test and a fair assessment of the user.

## How to run the application

First of all, you need to start docker desktop application on your local pc

You need to navigate to dir -> "dir_where_you_clone_the_project"/backend

After that you can run the application using this command -> docker-compose up -d

When all containers successfully started you can write in the browser uri -> http://localhost and the application should be successfully find

## Tech stack

### Backend: 
    - Asp Net 7
    - Ef Core
    - Serilog + Seq
    - OAuth2.0, Jwt Authorization/Authentication
    - Postgres
### Tests:
    - Benchmarks
    - Xunit + FluentAssertions
    
### Frontend: 
    - Html 
    - Scss 
    - Angular
    - Figma
    - Chart.Js
    
### Deployment Utils:
    - Docker
    - Github Actions
    - Digital Ocean
    
### Proxy:
    - Nginx
    
### Packages:
    - Mediator
    - FluentValidation
    - Mapster 
    - EfCore 7 
    - Serilog
    - Humanizer
    - Benchmark.Net
    
### Architecture: 
    - Monolith + DDD + CQRS

## How it looks like?
# - Accounts Pages (Sign-in, Sign-up)

### Sign-in Page:
   ![image](https://user-images.githubusercontent.com/80627757/233922663-e16f12ee-238e-47d8-b658-fe6093482638.png)
### Sign-up Page:
   ![image](https://user-images.githubusercontent.com/80627757/233922863-19dabeba-44ff-4068-a370-dba384548808.png)

# - Account Settings Page (Change username, password)

### AccountSettings Page:
   ![image](https://user-images.githubusercontent.com/80627757/233923066-b614ccba-3b36-4b29-80db-3dca15d8e8b7.png)
### Change username:
   ![image](https://user-images.githubusercontent.com/80627757/233923320-c637ff82-9952-4bd0-8ce9-8d9d610f9a94.png)
### Change password:  
   ![image](https://user-images.githubusercontent.com/80627757/233923349-c8daa989-1e27-417b-8996-a9cf492a026a.png)

# - Polls Pages (All polls, Polls-Pass)

### All Polls Page:
   ![image](https://user-images.githubusercontent.com/80627757/233923516-16780f11-e845-4268-bdf3-b2007357109e.png)
### Poll-Pass popup:
   ![image](https://user-images.githubusercontent.com/80627757/233923584-eede34a4-b820-4490-aef8-6f02d8514bd9.png)
### Poll-Pass page:
   ![image](https://user-images.githubusercontent.com/80627757/233927884-69c6e875-9567-4342-bb46-a61164b2a7a8.png)
### Thanksgiving page:
   ![image](https://user-images.githubusercontent.com/80627757/233927911-e4f4854f-9514-4751-b2fc-affbc5f46219.png)


# - My Polls Pages (GetAllPolls, Create poll, Edit poll, Check results with graphichs, Export to excel)

### My Polls Page:
   ![image](https://user-images.githubusercontent.com/80627757/233928166-698da532-fef6-4ee7-8cee-7d91c7399b37.png)
### Delete Poll popup:
   ![image](https://user-images.githubusercontent.com/80627757/233928878-6e4d7e8e-8a5f-4b0e-ba27-3e6c55f3c03c.png)
### Edit poll popups:
   ![image](https://user-images.githubusercontent.com/80627757/233928190-7acc705f-c3fc-46b5-90f0-39f2a449dae9.png)
   ![image](https://user-images.githubusercontent.com/80627757/233928230-ef169eaa-e0b0-42d5-a18b-14d1436f541f.png)

### Create poll popups:
   ![image](https://user-images.githubusercontent.com/80627757/233928337-60646c69-7a3c-4ee8-b921-a303fc166ee6.png)
   ![image](https://user-images.githubusercontent.com/80627757/233928492-47e8bb4e-a645-4b7f-b524-1873126a14f3.png)

### Check results of poll:
   ![image](https://user-images.githubusercontent.com/80627757/233928758-468eb929-f033-49ce-9f04-759e610344d3.png)
### Update||Delete result popups:
   ![image](https://user-images.githubusercontent.com/80627757/233928777-20f60da3-864e-418a-ba30-2a75bb19273a.png)
   ![image](https://user-images.githubusercontent.com/80627757/233928799-c87aa645-330f-432d-be9a-8c2415946903.png)

### Export statistics to excel popup:
![image](https://user-images.githubusercontent.com/80627757/233928966-7fae5418-6818-4e80-b312-33f939a72b09.png)




