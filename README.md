# Parcel-React Project-Builder w/ express & dotnet core backend alternatives.

A cra alternative, template builder for react apps with parcel and typescript.

As well as options for express backend as wrapper api combo,
 or even prebuilt auth for express and react.

Now you can replace the express backend with a dotnet core 3.1 backend wrapper.
Prebuilt auth for dotnet core 3.1 not included at the moment.

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

**Please be patient as generating the actual project might take a little while,
and the feedback isn't great. Wait for it to display done, no progress bar unfortunately.**

It will ask you which type of project you want to generate.

#### Currently there are four types included:

**cleanfrontend**: a parcel, react, typescript frontend with eslint and sass.

**withBackendWrapper**: an expressjs server, that serves api at /api, and static files,
from the client directory for everything else. Otherwise the client directory is identical to
the cleanfrontend.

**withAuth**: same as withBackendWrapper, except the backend has premade auth routes using passportjs,
and the frontend starts with a login form / register form, and redirects to the app when you are logged in.

**dotnetReact**: A dotnet core 3.1 wrapper project, where you can put your api's. Serving a parcel react empty frontend, at /. 
                x unit test folders also included, for those who wish to do unit and integration testing on the backend.
                The least optimal template, wish I could find a way to force all api's to be served at /api/{routedefinedincontroller},
                instead of {routedefinedincontroller}. Also unsure how well react-router will work with it, you might have to cheat by having a
                usestate with a string representing the current url, which you change when you press stuff in navbar. Then serving,
                different components in app based on that string. Would love to find a more optimal way of doing this template.
                Anyone have some tips contact me.
                For now I consider this *experimental*. This one might be better: [react-spa](https://github.com/NetCoreTemplates/react-spa).

## Running the app

#### Clean Frontend
To start cleanfrontend simply do npm start. 
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

#### Dotnet React
To start dotnetReact simply do
npm run preStart
that installs the npm packages neccesary in the Frontend folder as well as dotnet restore.

Therefore you only have to run it the first time, and everytime you add a new npm package to,
the package.json within Frontend, instead of using npm install --save within the client folder.

After installing the packages do 
                npm run server
To run the actual server serving the app.
It will hot-restart when you change files in frontend, or backend.

To test that the api works, and not only the frontend, you can do
localhost:5000/WeatherForecast on the dotnetReact app. If you get some json temperature forecast filler info,
you are all set.