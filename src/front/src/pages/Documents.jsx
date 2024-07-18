import DocumentCard from '../components/document'
import { useState, useEffect} from 'react';
import { Container, Button, Center,  useToast, Box, Alert, Spinner, AlertIcon  } from '@chakra-ui/react';
import {deleteDocument, fetchDocuments, createDocument} from '../services/document'
import ReactPaginate from 'react-paginate';
import FileDropzone from '../components/FileDropzone';


export default function Documents () 
{
	const [currentPage, setCurrentPage] = useState(0);
	const itemsPerPage = 10;
  const toast = useToast();
	const [documents, setDocuments] = useState([]);
	const [loading, setLoading] = useState(false);
	const [error, setError] = useState(null);

	useEffect(() => {
	  const fetchDocumentData = async () => {
		try {
		  const documents = await fetchDocuments();
		  setDocuments(documents);
		} catch (error) {
		  console.error("Failed to fetch documents:", error);
		}
	  };
	  fetchDocumentData();
	}, []); // [] ensures this effect runs only once

	const handleDelete =  (id) => {
		toast({
			title: 'Item Deleted',
			status: 'success',
			duration: 3000,
			isClosable: true,
		  });
		try {
			 deleteDocument(id);
			setDocuments(prevDoc => prevDoc.filter(emp => emp.id !== id));
		  } catch (error) {
			console.error("Failed to delete employee:", error);
		  }
	  };
	  const handleDrop = async (acceptedFiles) => {
		if (acceptedFiles.length > 0) {
		  const file = acceptedFiles[0];
		  const formData = new FormData();
		  formData.append('file', file);
	
		  setLoading(true);
		  setError(null);
	
		  try {
			await createDocument(formData);
		  } catch (e) {
			console.error('Ошибка при загрузке файла:', e);
			setError('Ошибка при загрузке файла');
		  } finally {
			setLoading(false);
		  }
		}
	  };
      const handlePageClick = ({ selected }) => {
        setCurrentPage(selected);
        };

      const displayDocuments = documents ?.slice(currentPage * itemsPerPage, (currentPage + 1) * itemsPerPage)
    .map((document) => (
	
      <li key={document.id} >
        <DocumentCard
			document = {document}
            onDelete={handleDelete}
        />
      </li>
    ));


    return(
      <Container maxW="container.lg">
		
				<FileDropzone onDrop={handleDrop} />
				<Center><h1>Documents</h1></Center>
			<Center>
			<Box  borderWidth="1px" borderRadius="lg" overflow="hidden" p={4} mb={4} w="full" maxW="md">
		<ul className="flex flex-col gap-5 w-full">
		  {displayDocuments}
		</ul>	
		</Box>
			</Center>

			{loading && <Spinner />}
      {error && (
        <Alert status="error">
          <AlertIcon />
          {error}
        </Alert>
      )}
		<ReactPaginate
		  previousLabel={'Previous'}
		  nextLabel={'Next'}
		  breakLabel={'...'}
		  breakClassName={'break-me'}
		  pageCount={Math.ceil(documents?.length / itemsPerPage)}
		  marginPagesDisplayed={2}
		  pageRangeDisplayed={5}
		  onPageChange={handlePageClick}
		  containerClassName={'pagination'}
		  subContainerClassName={'pages pagination'}
		  activeClassName={'active'}
		/>
		<br></br>
	  </Container>
       
    )
}