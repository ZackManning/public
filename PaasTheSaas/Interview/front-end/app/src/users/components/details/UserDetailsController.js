class UserDetailsController {

  /**
   * Constructor
   *
   * @param $log
   */
  constructor($log, $scope, UsersDataService, $mdDialog) {
    this.$log = $log;
    this.UsersDataService = UsersDataService;
    this.$mdDialog = $mdDialog;

    this.currentUser = angular.copy(this.selected);
    this.saving = false;
    this.deleting = false;

    $scope.$watch(() => this.selected, function (newValue, oldValue) {
      $scope.$ctrl.currentUser = angular.copy(newValue);
    });
  }

  onUserInfoChanged() {
    this.selected.pendingChanges = true;
  }

  save() {
    var self = this;
    self.saving = true;
    if (self.currentUser.id) {
      self.UsersDataService.updateUser(self.currentUser)
        .then(function (response) {
          self.handleUserSaved();
        }, function () {
          self.handleErrorSaving();
        })
        .finally(function () {
          self.saving = false;
        });
    }
    else {
      self.UsersDataService.createUser(self.currentUser)
        .then(function (response) {
          angular.copy(response.data, self.currentUser);
          self.handleUserSaved();
        }, function () {
          self.handleErrorSaving();
        })
        .finally(function () {
          self.saving = false;
        });
    }
  }

  handleUserSaved() {
    angular.copy(this.currentUser, this.selected);
    this.selected.pendingChanges = false;
  }

  handleErrorSaving() {
    self.$mdDialog.show(
      self.$mdDialog.alert()
        .title('Error Saving')
        .textContent('Failed to save user.')
        .ariaLabel('Error')
        .ok('OK')
    );
  }

  undoChanges() {
    if (this.selected.id) {
      this.selected.pendingChanges = false;
      angular.copy(this.selected, this.currentUser);
    }
    else {
      // This is a new user so just get rid of it.
      this.deleteUser(this.selected);
    }
  }

  moreMenu($mdMenu, ev) {
    $mdMenu.open(ev);
  }

  promptToDeleteCurrentUser() {
    var self = this;
    var confirm = self.$mdDialog.confirm()
      .title(`Confirm Deletion`)
      .textContent(`Are you sure you want to delete user '${self.selected.name || self.selected.githubHandle || ""}'?`)
      .ok('Delete User')
      .cancel('Cancel');
    self.$mdDialog.show(confirm).then(function () {
      self.deleteUser(self.selected);
    }, function () {
      // Do nothing
    });
  }

  deleteUser(user) {
    var self = this;
    if (user.id) {
      self.deleting = true;
      self.UsersDataService.deleteUser(user)
        .then(function () {
          self.handleUserDeleted({ user: user });
        }, function () {
          self.$mdDialog.show(
            self.$mdDialog.alert()
              .title('Error Deleting')
              .textContent('Failed to delete user.')
              .ariaLabel('Error')
              .ok('OK')
          );
        })
        .finally(function () {
          self.deleting = false;
        });
    }
    else {
      self.handleUserDeleted({ user: user });
    }
  }

}
export default UserDetailsController;
