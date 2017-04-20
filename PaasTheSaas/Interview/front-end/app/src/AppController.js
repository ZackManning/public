/**
 * Main App Controller for the Angular Material Starter App
 * @param UsersDataService
 * @param $mdSidenav
 * @constructor
 */
function AppController(UsersDataService, $mdSidenav, $scope, $mdDialog) {
  var self = this;

  self.selected = null;
  self.users = [];
  self.selectUser = selectUser;
  self.toggleList = toggleUsersList;

  // Load all registered users

  UsersDataService
    .loadAllUsers()
    .then(function (users) {
      self.users = [].concat(users);
      setSelectedUser(users[0]);
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

  /**
   * Select the current avatars
   * @param menuId
   */
  function selectUser(user) {
    if (user === self.selected) {
      return;
    }

    user = angular.isNumber(user) ? $scope.users[user] : user;

    if (self.selected.pendingChanges) {
      var confirm = $mdDialog.confirm()
        .title(`User "${self.selected.name}" has changed`)
        .textContent('Are you sure you want to switch to another user?')
        .ariaLabel('User changed')
        .ok('Discard changes')
        .cancel('Go back');
      $mdDialog.show(confirm).then(function () {
        self.selected.pendingChanges = false;
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
