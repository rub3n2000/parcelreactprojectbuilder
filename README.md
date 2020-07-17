# parcelreacttstemplate

A cra alternative, template builder for react apps with parcel and typescript.

As well as options for express backend as wrapper api combo,
 or even prebuilt auth for express and react.

works like cra, filling out the template for you with one command.

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

#### Currently there are three types included:

**cleanfrontend**: a parcel, react, typescript frontend with eslint and sass.

**withBackendWrapper**: an expressjs server, that serves api at /api, and static files,
from the client directory for everything else. Otherwise the client directory is identical to
the cleanfrontend.

**withAuth**: same as withBackendWrapper, except the backend has premade auth routes using passportjs,
and the frontend starts with a login form / register form, and redirects to the app when you are logged in.

## Running the app

To start cleanfrontend simply do npm start. 

To start withBackendWrapper or withAuth simply do
                npm run preStart
that installs the npm packages neccesary in the client folder as well as server.

Therefore you only have to run it the first time, and everytime you add a new npm package to,
the package.json within client, instead of using npm install --save within the client folder.

After installing the packages do 
                npm run server
To run the actual server serving the app.

To test that the api works, and not only the frontend, you can do
localhost:5000/api/test/health on the withBackendWrapper. If you get "all good" it works.

On withAuth you can tell simply by attempting to login or register, though here you will have to first
setup a mongodb database or equivelent in server.js. It is set by default to your local mongodb server,
if you happen to have one running you can just use that one.

Cleanfrontend doesn't include an api, so as long as the client appears to be working you are all good there.
