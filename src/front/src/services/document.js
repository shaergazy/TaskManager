import axios from "axios";
const Base_Url ="http://localhost:5001/api/"
export const fetchDocuments = async () => {
	try {
		var resposne = await axios.get(Base_Url + "documents");

		return resposne.data;
	} catch (e) {
		console.error(e);
	}
};

export const createDocument = async (document) => {
	try {
		var resposne = await axios.post(Base_Url + "documents", document, {
            headers: {
				'Content-Type': 'multipart/form-data',
              },
        });

		return resposne.status;
	} catch (e) {
		console.error(e);
	}
};

export const deleteDocument = async (id) => {
	try {
	  const response = await axios.delete(`${Base_Url}documents/${id}`, {
		headers: {
		  'Content-Type': 'application/json',
		},
	  });
	  return response.status;
	} catch (error) {
	  console.error(error);
	}
  };

