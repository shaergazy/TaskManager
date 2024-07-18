import axios from "axios";
const Base_Url ="http://localhost:5001/api/"
export const fetchEmployees = async (projectId = null) => {
	try {
		let url = Base_Url + "employees";
		if (projectId) {
		  url += `?projectId=${projectId}`;}
		  const response = await axios.get(url);
		  return response.data;
		  } catch (error) {
			console.error("Failed to fetch employees:", error);
		  }
};

export const createEmployee = async (employee) => {
	try {
		var resposne = await axios.post(Base_Url + "employees", employee, {
            headers: {
                'Content-Type': 'application/json',
              },
        });

		return resposne.status;
	} catch (e) {
		console.error(e);
	}
};

export const deleteEmployee = async (id) => {
	try {
	  const response = await axios.delete(`${Base_Url}employees/${id}`, {
		headers: {
		  'Content-Type': 'application/json',
		},
	  });
	  return response.status;
	} catch (error) {
	  console.error(error);
	}
  };

export const updateEmployee = async (id, employee) => {
	try {
	  const response = await axios.put(`${Base_Url}employees/`, employee, {
		headers: {
		  'Content-Type': 'application/json',
		},
	  });
	  return response.status;
	} catch (error) {
	  console.error(error);
	}
  };
