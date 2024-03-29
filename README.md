# Parcel-React Project-Builder w/ express & dotnet core backend alternatives.

**This is a hobby project made by one person and rarely updated. It's templates are meant as a starting point for projects,**
**I don't recommend using the code as is for production environments.**
**You are meant to build on the code, update dependencies to newer versions if you desire, exchange some packages for others that fit your needs etc.**

A cra alternative, template builder for react apps with parcel and typescript.

As well as options for express backend as wrapper api combo,
 or even prebuilt auth for express and react.

Now you can replace the express backend with a dotnet 6 backend wrapper.

works like cra, filling out the template for you with one command.

**Express backends are in js while frontends in ts. Feel free to change that.**
Personally my backends are usually quite simple, and my main use of typescript,
is being able to know what I will receive from the api, as the api defines the models,
with explicit typing for mongoose. As well as knowing what I am receiving in my props.

## Install instructions
        npm install -g parcelreacttstemplateinstaller
or
        npm install parcelreacttstemplateinstaller
*however removing -g will cause it to only work within your current folder.*

## Usage instructions
simply type: 
        **parcelreactGen**
in the terminal.

**Please be patient as generating the actual project might take a little while,**
**and the feedback isn't great. Wait for it to display done, no progress bar unfortunately.**

**All dotnet projects have a line in startup.cs that is commented out enforcing httpsredirect.**
**Should be uncommented before being published to a online host of any kind.**

**All dotnet auth projects have a appsettings.json with Token or RsaPrivateKey and RsaPublicKey.**
**Token should be replaced with a new random long string of characters, RsaPrivateKey and RsaPublicKey**
**should be replaced with newly generated private and public rsa key string values.**
**you can use my [very simple tool](https://github.com/rub3n2000/RsaKeyGenerator) for rsa keys or any other way of generating rsa key.**
**for the hmac Token which is in appsettings.json you can simply write a long random string or use an online string generator.**

**Nodejs auth project have a secret.js authKey which should be a new long random string.**

**It will ask you which type of project you want to generate.**

#### Currently there are 8 types included:

**cleanfrontend**: a parcel, react, typescript frontend with eslint and sass.

**withBackendWrapper**: an expressjs server, that serves api at /api, and static files,
from the client directory for everything else. Otherwise the client directory is identical to
the cleanfrontend.

**withAuth**: same as withBackendWrapper, except the backend has premade auth routes using passportjs,
and the frontend starts with a login form / register form, and redirects to the app when you are logged in.

**dotnet6React**: A dotnet 6.0 wrapper project, where you can put your api's. Serving a parcel react empty frontend, at /* and
                api at /api/{controllername}/{action}/{id}.
                only routes prefixed by /api/ will not return frontend.
                x unit test folders also included, for those who wish to do unit and integration testing on the backend.

**dotnet6ReactWOData**: A Dotnet 6.0 wrapper project, with OData. Serving a parcel react empty frontend at /* and
                       api at /api/{route}.
                       Only routes prefixed by /api/ will not return frontend.
                       x unit test folders also included, for those who wish to do unit and integration testing on the backend.

**dotnet6ReactwAuth** A Dotnet 6.0 wrapper project, with prebuilt auth in backend and frontend. It serves a parcel react frontend,
                     with a login form and register form, and a secret page you must login to see, at /*. While it serves  
                     api at /api/{route}.
                     Only routes prefixed by /api/ will not return frontend.
                     x unit test folders also included, for those who wish to do unit and integration testing on the backend.
                     Default database is a local sqlite db that gets created within the project. Feel free to replace it.

**dotnet6ReactwRsaAuth** A Dotnet 6.0 wrapper project, with prebuilt auth in backend and frontend. It serves a parcel react frontend,
                     with a login form and register form, and a secret page you must login to see, at /*. While it serves  
                     api at /api/{route}.
                     Only routes prefixed by /api/ will not return frontend.
                     x unit test folders also included, for those who wish to do unit and integration testing on the backend.
                     Default database is a local sqlite db that gets created within the project. Feel free to replace it.

**dotnet6ReactODataAuth** A Dotnet 6.0 wrapper project, with prebuilt auth in backend and frontend. It serves a parcel react frontend,
                     with a login form and register form, and a secret page you must login to see, at /*. While it serves  
                     api at /api/{route}.
                     Additionally, OData is implemented.
                     Only routes prefixed by /api/ will not return frontend.
                     x unit test folders also included, for those who wish to do unit and integration testing on the backend.
                     Default database is a local sqlite db that gets created within the project. Feel free to replace it.


## Running the app

#### Clean Frontend
To start cleanfrontend simply do npm install then npm start. 
It will hot-restart as you make changes.
Cleanfrontend doesn't include an api, so as long as the client appears to be working you are all good there.
_______________________________________________

#### Express Backend wrapper(withBackendWrapper) or withAuth
To start withBackendWrapper or withAuth simply do
                npm run preStart
that installs the npm packages neccesary in the client folder as well as server.

Therefore you only have to run it the first time, and everytime you add a new npm package to,
the package.json within client, instead of using npm install --save within the client folder.

After installing the packages do 
                npm run server
To run the actual server serving the app.
It will hot-restart when you change files in frontend, or backend.

To test that the api works, and not only the frontend, you can do
localhost:5000/api/test/health on the withBackendWrapper. If you get "all good" it works.

On withAuth you can tell simply by attempting to login or register, though here you will have to first
setup a mongodb database or equivelent in server.js. It is set by default to your local mongodb server,
if you happen to have one running you can just use that one.

________________________________________________________________________________________________________________

#### Dotnet6 React
To start dotnet6React simply do
npm run preStart
that installs the npm packages neccesary in the Frontend folder as well as dotnet restore.

Therefore you only have to run it the first time, and everytime you add a new npm package to,
the package.json within Frontend, instead of using npm install --save within the client folder.

After installing the packages do 
                npm run server
To run the actual server serving the app.
It will hot-restart when you change files in frontend, or backend.

To test that the api works, and not only the frontend, you can do
localhost:5000/api/WeatherForecast on the dotnetReact app. If you get some json temperature forecast filler info,
you are all set.

#### Dotnet6 React with OData.
To start dotnet6ReactwOData simply do
npm run preStart
that installs the npm packages neccesary in the Frontend folder as well as dotnet restore.

Therefore you only have to run it the first time, and everytime you add a new npm package to,
the package.json within Frontend, instead of using npm install --save within the client folder.

After installing the packages do 
                npm run server
To run the actual server serving the app.
It will hot-restart when you change files in frontend, or backend.

To test that the api works, and not only the frontend, you can do
localhost:5000/api/WeatherForecast on the dotnetReact app. If you get some json temperature forecast filler info,
you are all set.

You can then do localhost:5000/api/WeatherForecast?$Select=id to make sure odata is working.
Frontend should be at any route that doesnt start with /api/.

#### Dotnet6 React with auth.
To start dotnet6ReactwAuth simply do
npm run preStart
that installs the npm packages neccesary in the Frontend folder as well as dotnet restore, migrating db and updating it.
Therefore you only have to run it the first time.

If you use it again, you will have to delete the migration folder and the .db files first.

After installing the packages do 
                npm run server
To run the actual server serving the app.
It will hot-restart when you change files in frontend, or backend.

To test that the api works, and not only the frontend, you can simply attempt to register and then login at /*.

#### Dotnet6 React with auth RSA.
To start dotnet6ReactwRsaAuth simply do
npm run preStart
that installs the npm packages neccesary in the Frontend folder as well as dotnet restore, migrating db and updating it.
Therefore you only have to run it the first time.

If you use it again, you will have to delete the migration folder and the .db files first.

After installing the packages do 
                npm run server
To run the actual server serving the app.
It will hot-restart when you change files in frontend, or backend.

To test that the api works, and not only the frontend, you can simply attempt to register and then login at /*.

#### Dotnet6 React with OData and Auth.
To start dotnet6ReactODataAuth simply do
npm run preStart
that installs the npm packages neccesary in the Frontend folder as well as dotnet restore, migrating db and updating it.
Therefore you only have to run it the first time.

If you use it again, you will have to delete the migration folder and the .db files first.

After installing the packages do 
                npm run server
To run the actual server serving the app.
It will hot-restart when you change files in frontend, or backend.

To test that the api works, and not only the frontend, you can simply attempt to register and then login at /*.

You can then do localhost:5000/api/WeatherForecast?$Select=id to make sure odata is working.

________________________________________________________________________________________________________________