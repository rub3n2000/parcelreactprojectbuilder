# parcelreacttstemplate

A template for react apps with parcel and typescript.

## Install instructions
        npm install -g parcelreacttstemplateinstaller
or
        npm install parcelreacttstemplateinstaller
*however removing -g will cause it to only work within your current folder.*

## Usage instructions
simply type: 
        **parcelreactGen**
in the terminal.

It will ask you which type of project you want to generate.

#### Currently there are three types included:

**cleanfrontend**: a parcel, react, typescript frontend with eslint and sass.

**withBackendWrapper**: an expressjs server, that serves api at /api, and static files,
from the client directory for everything else. Otherwise the client directory is identical to
the cleanfrontend.

**withAuth**: same as withBackendWrapper, except the backend has premade auth routes using passportjs,
and the frontend starts with a login form / register form, and redirects to the app when you are logged in.