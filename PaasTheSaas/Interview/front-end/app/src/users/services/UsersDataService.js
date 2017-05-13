/**
 * Users DataService
 * Uses embedded, hard-coded data model; acts asynchronously to simulate
 * remote data service call(s).
 *
 * @returns {{loadAll: Function}}
 * @constructor
 */
function UsersDataService($http) {
  var baseApiUrl = 'http://localhost:50536/api/Users/';

  function loadAllUsers() {
    return $http.get(baseApiUrl);
  }

  function saveUser(user) {
    return $http.put(baseApiUrl + user.id, user);
  }

  return {
    loadAllUsers: loadAllUsers,
    saveUser: saveUser
  };
}

export default ['$http', UsersDataService];

