# Interview Project

This project uses **aspnet core** backend with an **angularjs** front end.  The purpose is to display your knowledge of developing a simple web application.  Complete as much as you can within 12 hours.

If you don't already have a Github account, create one now.

##### Code Setup

 Clone this repository and execute the following commands in a terminal:

* `git checkout -b master <my name>-interview`

The aspnet core application is in:

* `back-end`

The angularjs application is in: 

* `front-end`

- - -

### Back End -- aspnet core

##### Pre-requisites

This project assumes that you have any relevant development tools (like VS2017) already installed.

Make sure you have .NET Core 1.0+ installed and/or VS2017.  Install [here](https://www.microsoft.com/net/core)

#### Instructions:

Run the unit tests, they should all fail.  Implement the functionality so that they pass.  You may choose to store the data in any way you'd like and may modify the **IDataStore** as needed.

The project has swagger built-in so that you can test your functionality as you go.  When you debug the web api it should launch the swagger docs by default *http://localhost:<some-port>/api/docs*

- - -

### Front End -- angularjs

#### Getting Started

This project uses [jspm.io](http://jspm.io/), a package manager for SystemJS which is built on top of the dynamic ES6 module loader. This allows developers to load any module format (ES6, CommonJS, AMD, and globals).

You should use [AngularJS](https://angularjs.org/) and [Angular Material](https://material.angularjs.org/latest/) to complete this front end exercise.

##### Pre-requisites

This project assumes that you have NodeJS 6.x+ with npm 3+ and any relevant development tools (like VSCode) already installed.

##### Getting Environment Setup

To run the front end app navigate to the front-end directory and execute the following commands in a terminal:

* `npm install jspm live-server -g`
* `jspm update`
* `live-server --open=app`

> **Note:** You should use a web-server (like live-server above) to view your app in the browser. Open the dev console to see any warnings and browse the elements.

#### Instructions:

##### Step #1:

We need to store more information about the user.  Add fields for Address, City, State, Zip and Github Handle.

##### Step #2:

We'd like to be able to add a new user.  Create a button that allows the ability to create a new user.

##### Step #3:

We need to be able to update any selected user information.

##### Step #4:

I need to be able to delete users.  Add a way to do this with a confirmation dialog before it deletes the user.

##### Step #5:

Include tests.

- - -

### Putting it all together

The **angularjs** front end should connect to the **aspnet core** back end to retrieve and persist data.  Commit your code to your Github account so that we can review it when you are completed.

#### Bonus -- Deploy to Azure