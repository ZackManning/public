/**
 * Users DataService
 * Uses embedded, hard-coded data model; acts asynchronously to simulate
 * remote data service call(s).
 *
 * @returns {{loadAll: Function}}
 * @constructor
 */
function UsersDataService($http, $q) {
  var baseApiUrl = 'http://localhost:50536/api/Users/';

  function loadAllUsers() {
    return $http.get(baseApiUrl);
  }

  function saveUser(user) {
    if (user.id) {
      // Existing user so update
      return $http.put(baseApiUrl + user.id, user);
    }
    else {
      // New user so create
      return $http.post(baseApiUrl, user);
    }
  }

  function deleteUser(user) {
    if(user.id) {
      return $http.delete(user.id);
    }
    else {
      return $q.when(null);
    }
  }

  return {
    loadAllUsers: loadAllUsers,
    saveUser: saveUser
  };
}

export default ['$http', '$q', UsersDataService];

