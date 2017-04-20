class UserDetailsController {

  /**
   * Constructor
   *
   * @param $log
   */
  constructor($log, $scope, UsersDataService) {
    this.$log = $log;
    this.UsersDataService = UsersDataService;
    this.currentUser = angular.copy(this.selected);

    $scope.$watch(() => this.selected, function (newValue, oldValue) {
      $scope.$ctrl.currentUser = angular.copy(newValue);
    });
  }

  onUserInfoChanged() {
    this.selected.pendingChanges = true;
  }

  save() {
    var self = this;
    self.UsersDataService.saveUser(self.currentUser)
      .then(function(user) {
        angular.copy(self.currentUser, self.selected);
        console.log(self.selected);
      })
  }

  undoChanges() {
    this.selected.pendingChanges = false;
    angular.copy(this.selected, this.currentUser);
  }

}
export default UserDetailsController;
