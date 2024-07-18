import {
	Box,
	Button,
	VStack,
	Text,
  Center,
  Flex,
  } from '@chakra-ui/react';
  import moment from "moment/moment";

 const Employee = ({onDelete, employee, onEdit }) => {
  if (!employee) {
    return null;
  }
  const {id, firstName, lastName, email, phoneNumber, birthDate} = employee;
	return (
    <Center>
<Box borderWidth="1px" borderRadius="lg" overflow="hidden" p={4} mb={4} w="full" maxW="md"> 
		<VStack align="stretch" spacing={4}>
            <Text fontWeight="bold">
              Full name: {firstName} {lastName}
            </Text>
            <Text>Email: {email}</Text>
            <Text>Phone Number: {phoneNumber}</Text>
            <Text>Birth Date: {moment(birthDate).format("DD/MM/YYYY")}</Text>
            <Flex>
            <Button colorScheme="red" mr={2} onClick={() => onDelete(id)}>Delete</Button>
        <Button colorScheme="blue" onClick={() => onEdit(employee)}>Edit</Button>
            </Flex>
  </VStack>
    </Box>
    </Center>
    

);
}
export default Employee;
