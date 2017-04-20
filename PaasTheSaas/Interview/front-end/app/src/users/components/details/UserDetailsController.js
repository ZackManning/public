class UserDetailsController {

  /**
   * Constructor
   *
   * @param $log
   */
  constructor($log, $scope) {
    this.$log = $log;
    this.changed = false;

    // $scope.$watch(() => this.selected, function (newValue, oldValue) {
    //   $scope.$ctrl.changed = false;
    // });
  }

  onUserInfoChanged() {
    this.selected.changed = true;
  }

}
export default UserDetailsController;

