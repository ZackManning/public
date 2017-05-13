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
    self.UsersDataService.saveUser(self.currentUser)
      .then(function (user) {
        angular.copy(self.currentUser, self.selected);
      }, function () {
        self.$mdDialog.show(
          self.$mdDialog.alert()
            .title('Error Saving')
            .textContent('Failed to save user.')
            .ariaLabel('Error')
            .ok('OK')
        );
      })
      .finally(function () {
        self.saving = false;
      });
  }

  undoChanges() {
    if (this.selected.id) {
      this.selected.pendingChanges = false;
      angular.copy(this.selected, this.currentUser);
    }
    else {
      // This is a new user so just get rid of it.
      this.deleteUser({ user: this.selected });
    }
  }

}
export default UserDetailsController;
