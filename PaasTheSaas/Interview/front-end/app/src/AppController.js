/**
 * Main App Controller for the Angular Material Starter App
 * @param UsersDataService
 * @param $mdSidenav
 * @constructor
 */
function AppController(UsersDataService, $mdSidenav, $scope, $mdDialog) {
  var self = this;

  self.selected = null;
  self.users = null;
  self.selectUser = selectUser;
  self.addUser = addUser;
  self.deleteUser = deleteUser;
  self.toggleList = toggleUsersList;

  // Load all registered users

  UsersDataService
    .loadAllUsers()
    .then(function (response) {
      self.users = [].concat(response.data);
      if (self.users.length > 0) {
        setSelectedUser(self.users[0]);
      }
    });

  // *********************************
  // Internal methods
  // *********************************

  /**
   * Hide or Show the 'left' sideNav area
   */
  function toggleUsersList() {
    $mdSidenav('left').toggle();
  }

  function setSelectedUser(user) {
    self.selected = user;
  }

  function addUser() {
    var newUser = {
      id: null,
      githubHandle: null,
      pendingChanges: true
    };

    self.users.push(newUser);
    selectUser(newUser);
  }

  function deleteUser(user) {
    console.log(user);
    if (user.id) {
      self.UsersDataService.deleteUser(user)
        .then(function () {
          removeUserFromArray(user);
        });
    }
    else {
      removeUserFromArray(user);
    }
  }

  function removeUserFromArray(user) {
    if (self.selected === user) {
      self.selected.pendingChanges = false;
      selectUser(null);
    }
    self.users = self.users.filter(u => u !== user);
  }

  /**
   * Select the current avatars
   * @param menuId
   */
  function selectUser(user) {
    if (user === self.selected) {
      return;
    }

    user = angular.isNumber(user) ? $scope.users[user] : user;

    if (self.selected && self.selected.pendingChanges) {
      var confirm = $mdDialog.confirm()
        .title(`User "${self.selected.name || self.selected.githubHandle || ""}" has changed`)
        .textContent('Are you sure you want to switch to another user?')
        .ariaLabel('User changed')
        .ok('Discard changes')
        .cancel('Go back');
      $mdDialog.show(confirm).then(function () {
        if (self.selected.id) {
          self.selected.pendingChanges = false;
        }
        else {
          // This is a new user so just get rid of it.
          removeUserFromArray(self.selected);
        }
        setSelectedUser(user);
      }, function () {
        // Do nothing
      });
    }
    else {
      setSelectedUser(user);
    }
  }
}

export default ['UsersDataService', '$mdSidenav', '$scope', '$mdDialog', AppController];
