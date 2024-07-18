import Employee from '../components/Employee';
import CreateEmployeeModal from '../components/createEmployeeModal'
import { useState, useEffect} from 'react';
import {createEmployee, fetchEmployees, updateEmployee, deleteEmployee } from '../services/employee'
import { Container, Button, Center,  useToast, Box  } from '@chakra-ui/react';
import ReactPaginate from 'react-paginate';
import EditEmployeeModal from '../components/editEmployeeModal';
import { useParams } from 'react-router-dom';

export default function Employees () 
{
  const { projectId } = useParams();
	const [currentPage, setCurrentPage] = useState(0);
	const itemsPerPage = 3;
	const [isEditModalOpen, setIsEditModalOpen] = useState(false);
	const [isCreateModalOpen, setIsCreateModalOpen] = useState(false);
	const [selectedEmployee, setSelectedEmployee] = useState(null);
  const toast = useToast();
	const [employees, setEmployees] = useState([]);

	useEffect(() => {
		const fetchEmployeesData = async () => {
		  try {
			const employees = await fetchEmployees(projectId);
			setEmployees(employees);
		  } catch (error) {
			console.error("Failed to fetch employees:", error);
		  }
		};
	
		fetchEmployeesData();
	  }, []);
      const onCreate = async (employee) => {
        try {
          await createEmployee(employee);
          setEmployees(prevEmployees => [...prevEmployees, Employee]);
        } catch (error) {
          console.error("Failed to create Project:", error);
        }
      };
    
      const onDelete = async (id) => {
        try {
          await deleteEmployee(id);
          setEmployees(prevEmployees => prevEmployees.filter(emp => emp.id !== id));
        } catch (error) {
          console.error("Failed to delete employee:", error);
        }
      };
      const handlePageClick = ({ selected }) => {
        setCurrentPage(selected);
        };

      const onEdit = (employee) => {
        setSelectedEmployee(employee);
        setIsEditModalOpen(true);
        };

        const onSaveEdit = async (updatedEmployee) => {
          await updateEmployee(updatedEmployee.id, updatedEmployee);
          setEmployees(employees?? "".map(employee => employee.id === updatedEmployee.id ? updatedEmployee : employee));
          };

      const displayEmployees = employees ?.slice(currentPage * itemsPerPage, (currentPage + 1) * itemsPerPage)
    .map((employee) => (
	
      <li key={employee.id} >
        <Employee
			onEdit={onEdit}
			onDelete={onDelete}
			employee={employee}
        />
      </li>
    ));


    return(
      <Container maxW="container.lg">
			<Center>
			<Box  borderWidth="1px" borderRadius="lg" overflow="hidden" p={4} mb={4} w="full" maxW="md">
		<ul className="flex flex-col gap-5 w-full">
		  {displayEmployees}
		</ul>	
		</Box>
			</Center>

  
		<ReactPaginate
		  previousLabel={'Previous'}
		  nextLabel={'Next'}
		  breakLabel={'...'}
		  breakClassName={'break-me'}
		  pageCount={Math.ceil(employees?.length / itemsPerPage)}
		  marginPagesDisplayed={2}
		  pageRangeDisplayed={5}
		  onPageChange={handlePageClick}
		  containerClassName={'pagination'}
		  subContainerClassName={'pages pagination'}
		  activeClassName={'active'}
		/>
		<br></br>
        <Center><Button borderWidth="1px" borderRadius="lg" overflow="hidden" p={4} mb={4} w="full" maxW="md"  colorScheme="blue" 
          onClick={() => setIsCreateModalOpen(true)}>Create</Button></Center>
        {selectedEmployee && (
        <EditEmployeeModal
          isOpen={isEditModalOpen}
          onClose={() => setIsEditModalOpen(false)}
          employee={selectedEmployee}
          onSave={onSaveEdit}
        />
      )}
	  		<CreateEmployeeModal
			isOpen={isCreateModalOpen}
			onClose={() => setIsCreateModalOpen(false)}
			onCreate={onCreate}
			/>
	  </Container>
       
    )
}