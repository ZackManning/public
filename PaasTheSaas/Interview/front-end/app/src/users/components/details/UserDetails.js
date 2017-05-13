import UserDetailsController from './UserDetailsController'

export default {
  name : 'userDetails',
  config : {
    bindings         : {  selected: '<', deleteUser: '&deleteUser' },
    templateUrl      : 'src/users/components/details/UserDetails.html',
    controller       : [ '$log', '$scope', 'UsersDataService', '$mdDialog', UserDetailsController ]
  }
};
